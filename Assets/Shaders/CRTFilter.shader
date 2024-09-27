Shader "Hidden/CRTFilterWithContrastEnhancement"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Colorize ("Colorize", Range(0,1)) = 1.0 // Уже добавлен ранее
    }
    SubShader
    {
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_TexelSize;
            static const half pi = 3.141592653589793238462;

            // Параметры шейдера
            uniform float m_time;
            uniform fixed m_screenBend;
            uniform fixed m_screenOverscan;
            uniform fixed m_resX;
            uniform fixed m_resY;
            uniform fixed m_shadowlines;
            uniform fixed m_shadowlinesSpeed;
            uniform fixed m_shadowlinesAlpha;
            uniform fixed m_vignetteSize;
            uniform fixed m_vignetteSmooth;
            uniform fixed m_vignetteRound;
            uniform fixed m_noiseSize;
            uniform fixed m_noiseAlpha;
            uniform fixed m_noiseSpeed;
            uniform fixed m_brightness;
            uniform fixed m_contrast;
            uniform fixed m_gamma;
            uniform fixed m_red;
            uniform fixed m_green;
            uniform fixed m_blue;
            uniform fixed2 m_redOffset;
            uniform fixed2 m_greenOffset;
            uniform fixed2 m_blueOffset;

            // Удаляем Contrast Power
            // uniform float m_contrastPower; // Удалить

            // Новый параметр для цветизации
            uniform float m_colorize; // Диапазон от 0 до 1

            half2 m_pixSize;

            // Вершинный шейдер
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            // Функция десатурации
            fixed3 Desaturate(fixed3 color)
            {
                fixed gray = dot(color, fixed3(0.299, 0.587, 0.114));
                return fixed3(gray, gray, gray);
            }

            half2 pixel_size()
            {
                return half2((_MainTex_TexelSize.z / m_resX) * _MainTex_TexelSize.x,
                             (_MainTex_TexelSize.w / m_resY) * _MainTex_TexelSize.y);
            }
            half2 pixel_part(half2 uv)
			{
				return half2(floor(fmod(uv.x, m_pixSize.x) * _MainTex_TexelSize.z), floor(fmod(uv.y, m_pixSize.y) * _MainTex_TexelSize.w));
			}
			half pixel_part_x(half uvx)
			{
				return floor(fmod(uvx, m_pixSize.x) * _MainTex_TexelSize.z);
			}
			half pixel_part_y(half uvy)
			{
				return floor(fmod(uvy, m_pixSize.y) * _MainTex_TexelSize.w);
			}

			half2 pixel_frac(half2 uv)
			{
				return half2(fmod(uv.x, m_pixSize.x) * m_resX, fmod(uv.y, m_pixSize.y) * m_resY);
			}
			half pixel_frac_x(half uvx)
			{
				return fmod(uvx, m_pixSize.x) * m_resX;
			}

            half pixel_frac_y(half uvy)
            {
                return fmod(uvy, m_pixSize.y) * m_resY;
            }

            fixed2 pixel_num(half2 uv)
			{
				return fixed2(floor(uv.x / m_pixSize.x), floor(uv.y / m_pixSize.y));
			}
			fixed pixel_num_x(half uvx)
			{
				return floor(uvx / m_pixSize.x);
			}
			fixed pixel_num_y(half uvy)
			{
				return floor(uvy / m_pixSize.y);
			}

            half random (half2 uv)
            {
                return frac(sin(dot(uv,half2(12.4898,78.233)))	* 43758.541987 * sin(m_time * m_noiseSpeed));
            }

            half noise(half2 uv)
			{
				half2 i = floor(uv);
				half2 f = frac(uv);

				half a = random(i);
				half b = random(i + half2(1., 0.));
				half c = random(i + half2(0, 1.));
				half d = random(i + half2(1., 1.));

				half2 u = smoothstep(0., 1., f);

				return lerp(a, b, u.x) + (c - a) * u.y * (1. - u.x) + (d - b) * u.x * u.y;
			}

            half vignette(half2 uv)
			{
				uv -= .5;
				uv *= m_vignetteSize;
				half amount = 1. - sqrt(pow(abs(uv.x), m_vignetteRound) + pow(abs(uv.y), m_vignetteRound));				

				return smoothstep(0, m_vignetteSmooth, amount);
			}

            half crt_line(half i, half lines, half speed)
			{
				return sin(i * lines * pi + speed * m_time);
			}

            half2 screen_bend(half2 uv)
            {
                uv -= 0.5;
                uv *= 2.0;
                uv.x *= 1.0 + pow(uv.y / m_screenBend, 2.0) - m_screenOverscan;
                uv.y *= 1.0 + pow(uv.x / m_screenBend, 2.0) - m_screenOverscan;
                uv /= 2.0;
                return uv + 0.5;
            }

            // Функция для смешивания цветов
            fixed3 BlendColor(fixed3 desaturatedCol, fixed3 originalCol, float colorize)
            {
                return lerp(desaturatedCol, originalCol, colorize);
            }

            // Фрагментный шейдер
            fixed4 frag(v2f i) : SV_Target
            {
                m_pixSize = pixel_size();

                half2 buv = screen_bend(i.uv);

                fixed4 col = tex2D(_MainTex, buv);

                fixed4 originalCol = col; // Сохраняем исходный цвет

                col.r += tex2D(_MainTex, buv + m_redOffset).r;
                col.g += tex2D(_MainTex, buv + m_greenOffset).g;
                col.b += tex2D(_MainTex, buv + m_blueOffset).b;
                col.rgb /= 2.0;


                // Применяем десатурацию для получения градаций серого
                fixed3 desaturatedCol = Desaturate(col.rgb);

                // Применяем яркость и контрастность
                desaturatedCol = m_contrast * (desaturatedCol - 0.5) + 0.5 + m_brightness;

                // Смешиваем десатурированный цвет с исходным на основе m_colorize
                fixed3 finalCol = BlendColor(desaturatedCol, originalCol.rgb, m_colorize);

                // Продолжаем с шумом и другими эффектами
                finalCol = lerp(finalCol, fixed3(noise(buv * m_noiseSize), noise(buv * m_noiseSize), noise(buv * m_noiseSize)), m_noiseAlpha);
                finalCol = lerp(finalCol, fixed3(crt_line(buv.y, m_shadowlines, m_shadowlinesSpeed), crt_line(buv.y, m_shadowlines, m_shadowlinesSpeed), crt_line(buv.y, m_shadowlines, m_shadowlinesSpeed)), m_shadowlinesAlpha);

                // Применяем виньетку
                return fixed4(finalCol, col.a) * vignette(i.uv);
            }

            ENDCG
        }
    }
}