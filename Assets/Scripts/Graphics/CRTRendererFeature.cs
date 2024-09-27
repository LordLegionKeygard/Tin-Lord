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

        [Header("Main")]

        [Range(0f, 10f)]
        public float screenBend = 4f;

        [Range(0f, 10f)]
        public float vignetteSize = 5.3f;
        [Range(0f, 20f)]
        public float vignetteSmooth = 2;
        [Range(2f, 50f)]
        public float vignetteRound = 25f;

        [Header("Contrast Enhancement")]
        [Range(-0.5f, 0.5f)]
        public float brightness = 0f; // Можно настроить
        [Range(0.5f, 2f)]
        public float contrast = 1f; // Можно настроить
        // Удаляем contrastPower
        // [Range(1f, 10f)]
        // public float contrastPower = 2f; // Удалить

        [Header("Colorize")] // Новый раздел в инспекторе
        [Range(0f, 1f)]
        public float colorize = 1f; // Новый параметр

        private CRTRenderPass crtRenderPass;
        private Material material;

        public override void Create()
        {
            if (material == null)
                material = new Material(shader);

            if (crtRenderPass == null)
                crtRenderPass = new CRTRenderPass();
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            if (material == null || crtRenderPass == null)
                return;

            // Передача параметров CRT в материал
            material.SetFloat("m_screenBend", screenBend == 0 ? 1000 : 13 - screenBend);
            material.SetFloat("m_screenOverscan", 0f);
            material.SetFloat("m_blur", 0f);
            material.SetFloat("m_smidge", 0f);
            material.SetFloat("m_bleedr", 0f);
            material.SetFloat("m_bleedg", 0f);
            material.SetFloat("m_bleedb", 0f);

            material.SetFloat("m_resX", pixelResolutionX);
            material.SetFloat("m_resY", pixelResolutionY);

            material.SetFloat("m_scanlinesStrength", 0f);
            material.SetFloat("m_apertureStrength", 0f);
            material.SetFloat("m_shadowlines", 0f);
            material.SetFloat("m_shadowlinesSpeed", 0f);
            material.SetFloat("m_shadowlinesAlpha", 0f);

            material.SetFloat("m_vignetteSize", vignetteSize * 0.35f);
            material.SetFloat("m_vignetteSmooth", vignetteSmooth * 0.1f);
            material.SetFloat("m_vignetteRound", vignetteRound);

            material.SetFloat("m_noiseSize", 0f);
            material.SetFloat("m_noiseAlpha", 0f);
            material.SetFloat("m_noiseSpeed", 0f);

            material.SetFloat("m_brightness", brightness);
            material.SetFloat("m_contrast", contrast);
            material.SetFloat("m_gamma", 1f);

            material.SetFloat("m_red", 1f);
            material.SetFloat("m_green", 1f);
            material.SetFloat("m_blue", 1f);

            material.SetVector("m_redOffset", Vector2.zero);
            material.SetVector("m_greenOffset", Vector2.zero);
            material.SetVector("m_blueOffset", Vector2.zero);

            // Передача параметров contrast и colorize
            material.SetFloat("m_contrast", contrast);
            material.SetFloat("m_colorize", colorize); // Новый параметр

            // Передача времени в шейдер
            material.SetFloat("m_time", Time.time);

            crtRenderPass.Init(renderer, material);
            renderer.EnqueuePass(crtRenderPass);
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
                if (cameraColorTexture == new RenderTargetIdentifier(BuiltinRenderTextureType.CameraTarget))
                {
                    // Предупреждение, если требуется
                    return;
                }

                CommandBuffer cmd = CommandBufferPool.Get(PROFTAG);

                cmd.Blit(cameraColorTexture, tempRT, material, 0);
                cmd.Blit(tempRT, cameraColorTexture);

                context.ExecuteCommandBuffer(cmd);
                cmd.Clear();

                CommandBufferPool.Release(cmd);
            }
        }
    }
}