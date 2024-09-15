using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace CRTFilter
{
    public class CRTRendererFeature : ScriptableRendererFeature
    {
        public Shader shader;

        private float pixelResolutionX = 1920;
        private float pixelResolutionY = 1080;

        [Range(0f, 10f)]
        public float screenBend = 4f;

        [Range(0f, 10f)]
        public float vignetteSize = 5.3f;
        [Range(0f, 20f)]
        public float vignetteSmooth = 2;
        [Range(2f, 50f)]
        public float vignetteRound = 25f;

        private CRTRenderPass crtRenderPass;
        private Material material;


        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            if (material == null || crtRenderPass == null)
                return;

            material.SetFloat("m_screenBend", screenBend == 0 ? 1000 : 13 - screenBend);
            material.SetFloat("m_resX", pixelResolutionX);
            material.SetFloat("m_resY", pixelResolutionY);
            material.SetFloat("m_vignetteSize", vignetteSize * 0.35f);
            material.SetFloat("m_vignetteSmooth", vignetteSmooth * 0.1f);
            material.SetFloat("m_vignetteRound", vignetteRound);
            material.SetFloat("m_contrast", 1);
            material.SetFloat("m_gamma", 1);
            material.SetFloat("m_red", 1);
            material.SetFloat("m_green", 1);
            material.SetFloat("m_blue", 1);

            crtRenderPass.Init(renderer, material);
            renderer.EnqueuePass(crtRenderPass);
        }

        public override void Create()
        {
            if (material == null)
                material = new Material(shader);

            if (crtRenderPass == null)
                crtRenderPass = new CRTRenderPass();
        }

        protected override void Dispose(bool disposing)
        {
            if (crtRenderPass != null)
                crtRenderPass = null;
            if (material != null)
            {
                CoreUtils.Destroy(material);
                material = null;
            }
        }

        class CRTRenderPass : ScriptableRenderPass
        {
            private const string PROFTAG = "CRTFilter";

            private ScriptableRenderer renderer;
            private Material material;
            private RenderTargetIdentifier cameraRT;
            private RenderTargetIdentifier tempRT;

            public CRTRenderPass()
            {
                renderPassEvent = RenderPassEvent.AfterRenderingPostProcessing;
            }

            public void Init(ScriptableRenderer renderer, Material material)
            {
                this.renderer = renderer;
                this.material = material;
            }

            public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
            {
                var width = cameraTextureDescriptor.width;
                var height = cameraTextureDescriptor.height;

                var textureId = Shader.PropertyToID("_CRTFilterTexture");
                cmd.GetTemporaryRT(textureId, width, height, 0, FilterMode.Point, RenderTextureFormat.ARGB32);
                tempRT = new RenderTargetIdentifier(textureId);
                ConfigureTarget(tempRT);
            }

            public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
            {
                if (material == null)
                    return;

                var cameraColorTexture = renderingData.cameraData.renderer.cameraColorTarget;
                if ((cameraColorTexture == new RenderTargetIdentifier(BuiltinRenderTextureType.CameraTarget)))
                {
                    // Debug.LogWarning("CRT Filter: camera doesn't render to the texture. Please make sure, that there is PixelPerfectCamera component attached with CropFrame setting anything but 'None'");
                    return;
                }

                CommandBuffer cmd = CommandBufferPool.Get(PROFTAG);

                material.SetFloat("m_time", Time.time);
                cmd.Blit(cameraColorTexture, tempRT, material, 0);
                cmd.Blit(tempRT, cameraColorTexture);

                context.ExecuteCommandBuffer(cmd);
                cmd.Clear();

                CommandBufferPool.Release(cmd);
            }
        }
    }
}