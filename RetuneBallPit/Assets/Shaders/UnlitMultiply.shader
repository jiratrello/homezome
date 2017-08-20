// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.28 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.28;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.06,fgrn:0,fgrf:9.87,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:34164,y:32779,varname:node_3138,prsc:2|emission-2699-OUT;n:type:ShaderForge.SFN_Tex2d,id:6647,x:33025,y:33023,ptovrint:False,ptlb:Texture,ptin:_Texture,varname:node_6647,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:b7989a7257e77a04891748387795b5c9,ntxv:3,isnm:False;n:type:ShaderForge.SFN_Lerp,id:7600,x:33376,y:32891,varname:node_7600,prsc:2|A-9058-OUT,B-1253-OUT,T-5086-OUT;n:type:ShaderForge.SFN_Color,id:6085,x:32179,y:32708,ptovrint:False,ptlb:ColourA,ptin:_ColourA,varname:node_6085,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Color,id:4765,x:32179,y:32923,ptovrint:False,ptlb:ColourB,ptin:_ColourB,varname:node_4765,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0,c3:1,c4:1;n:type:ShaderForge.SFN_Lerp,id:2699,x:33940,y:33029,varname:node_2699,prsc:2|A-5086-OUT,B-7600-OUT,T-2286-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2286,x:32764,y:33296,ptovrint:False,ptlb:Crossfade,ptin:_Crossfade,varname:node_2286,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_RgbToHsv,id:8070,x:32360,y:32923,varname:node_8070,prsc:2|IN-4765-RGB;n:type:ShaderForge.SFN_Add,id:7751,x:32525,y:33026,varname:node_7751,prsc:2|A-8070-HOUT,B-2784-OUT;n:type:ShaderForge.SFN_HsvToRgb,id:1253,x:32754,y:32955,varname:node_1253,prsc:2|H-7751-OUT,S-8070-SOUT,V-8070-VOUT;n:type:ShaderForge.SFN_RgbToHsv,id:3829,x:32348,y:32708,varname:node_3829,prsc:2|IN-6085-RGB;n:type:ShaderForge.SFN_Add,id:4523,x:32551,y:32805,varname:node_4523,prsc:2|A-3829-HOUT,B-2784-OUT;n:type:ShaderForge.SFN_HsvToRgb,id:9058,x:32715,y:32713,varname:node_9058,prsc:2|H-4523-OUT,S-3829-SOUT,V-3829-VOUT;n:type:ShaderForge.SFN_OneMinus,id:1131,x:32905,y:33352,varname:node_1131,prsc:2|IN-2286-OUT;n:type:ShaderForge.SFN_Multiply,id:6204,x:33174,y:33362,varname:node_6204,prsc:2|A-1131-OUT,B-4283-OUT;n:type:ShaderForge.SFN_Vector1,id:4283,x:32958,y:33512,varname:node_4283,prsc:2,v1:0;n:type:ShaderForge.SFN_Tex2d,id:420,x:33025,y:33203,ptovrint:False,ptlb:NoiseTexture,ptin:_NoiseTexture,varname:node_420,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:5086,x:33301,y:33099,varname:node_5086,prsc:2|A-6647-RGB,B-420-RGB;n:type:ShaderForge.SFN_Time,id:2473,x:32043,y:33133,varname:node_2473,prsc:2;n:type:ShaderForge.SFN_Multiply,id:2784,x:32268,y:33165,varname:node_2784,prsc:2|A-2473-TSL,B-1723-OUT;n:type:ShaderForge.SFN_Vector1,id:1723,x:32081,y:33308,varname:node_1723,prsc:2,v1:0.1;proporder:6647-6085-4765-2286-420;pass:END;sub:END;*/

Shader "Shader Forge/UnlitMultiply" {
    Properties {
        _Texture ("Texture", 2D) = "bump" {}
        _ColourA ("ColourA", Color) = (1,0,0,1)
        _ColourB ("ColourB", Color) = (0,0,1,1)
        _Crossfade ("Crossfade", Float ) = 0
        _NoiseTexture ("NoiseTexture", 2D) = "white" {}
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _Texture; uniform float4 _Texture_ST;
            uniform float4 _ColourA;
            uniform float4 _ColourB;
            uniform float _Crossfade;
            uniform sampler2D _NoiseTexture; uniform float4 _NoiseTexture_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos(v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float4 _Texture_var = tex2D(_Texture,TRANSFORM_TEX(i.uv0, _Texture));
                float4 _NoiseTexture_var = tex2D(_NoiseTexture,TRANSFORM_TEX(i.uv0, _NoiseTexture));
                float3 node_5086 = (_Texture_var.rgb*_NoiseTexture_var.rgb);
                float4 node_3829_k = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
                float4 node_3829_p = lerp(float4(float4(_ColourA.rgb,0.0).zy, node_3829_k.wz), float4(float4(_ColourA.rgb,0.0).yz, node_3829_k.xy), step(float4(_ColourA.rgb,0.0).z, float4(_ColourA.rgb,0.0).y));
                float4 node_3829_q = lerp(float4(node_3829_p.xyw, float4(_ColourA.rgb,0.0).x), float4(float4(_ColourA.rgb,0.0).x, node_3829_p.yzx), step(node_3829_p.x, float4(_ColourA.rgb,0.0).x));
                float node_3829_d = node_3829_q.x - min(node_3829_q.w, node_3829_q.y);
                float node_3829_e = 1.0e-10;
                float3 node_3829 = float3(abs(node_3829_q.z + (node_3829_q.w - node_3829_q.y) / (6.0 * node_3829_d + node_3829_e)), node_3829_d / (node_3829_q.x + node_3829_e), node_3829_q.x);;
                float4 node_2473 = _Time + _TimeEditor;
                float node_2784 = (node_2473.r*0.1);
                float4 node_8070_k = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
                float4 node_8070_p = lerp(float4(float4(_ColourB.rgb,0.0).zy, node_8070_k.wz), float4(float4(_ColourB.rgb,0.0).yz, node_8070_k.xy), step(float4(_ColourB.rgb,0.0).z, float4(_ColourB.rgb,0.0).y));
                float4 node_8070_q = lerp(float4(node_8070_p.xyw, float4(_ColourB.rgb,0.0).x), float4(float4(_ColourB.rgb,0.0).x, node_8070_p.yzx), step(node_8070_p.x, float4(_ColourB.rgb,0.0).x));
                float node_8070_d = node_8070_q.x - min(node_8070_q.w, node_8070_q.y);
                float node_8070_e = 1.0e-10;
                float3 node_8070 = float3(abs(node_8070_q.z + (node_8070_q.w - node_8070_q.y) / (6.0 * node_8070_d + node_8070_e)), node_8070_d / (node_8070_q.x + node_8070_e), node_8070_q.x);;
                float3 emissive = lerp(node_5086,lerp((lerp(float3(1,1,1),saturate(3.0*abs(1.0-2.0*frac((node_3829.r+node_2784)+float3(0.0,-1.0/3.0,1.0/3.0)))-1),node_3829.g)*node_3829.b),(lerp(float3(1,1,1),saturate(3.0*abs(1.0-2.0*frac((node_8070.r+node_2784)+float3(0.0,-1.0/3.0,1.0/3.0)))-1),node_8070.g)*node_8070.b),node_5086),_Crossfade);
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
