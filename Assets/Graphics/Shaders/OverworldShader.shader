Shader "OverworldShader"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        [HideInInspector] _Color ("Tint", Color) = (1,1,1,1)
        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
        [HideInInspector] _RendererColor ("RendererColor", Color) = (1,1,1,1)
        [HideInInspector] _Flip ("Flip", Vector) = (1,1,1,1)
        [PerRendererData] _AlphaTex ("External Alpha", 2D) = "white" {}
        [PerRendererData] _EnableExternalAlpha ("Enable External Alpha", Float) = 0

        _HueShift ("Hue Shift", Range(0,1)) = 1.0 // 1 = full color, 0 = grayscale
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend One OneMinusSrcAlpha

        Pass
        {
        CGPROGRAM
            #pragma vertex SpriteVert
            #pragma fragment SpriteFragWithHue
            #pragma target 2.0
            #pragma multi_compile_instancing
            #pragma multi_compile_local _ PIXELSNAP_ON
            #pragma multi_compile _ ETC1_EXTERNAL_ALPHA
            #include "UnitySprites.cginc"

            uniform float _HueShift;

            fixed3 ToGrayscale(fixed3 color)
            {
                float gray = dot(color, fixed3(0.299, 0.587, 0.114)); // Standard grayscale conversion
                return fixed3(gray, gray, gray);
            }

            fixed4 SpriteFragWithHue(v2f IN) : SV_Target
            {
                fixed4 c = SampleSpriteTexture(IN.texcoord) * IN.color; // Sample color
                fixed3 gray = ToGrayscale(c.rgb);
                c.rgb = lerp(gray, c.rgb, _HueShift); // Apply hue shift
                
                // Correct alpha blending by multiplying RGB by alpha
                c.rgb *= c.a;

                return c;
            }
        ENDCG
        }
    }
}

