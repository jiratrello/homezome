// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.28 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.28;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.2352941,fgcg:0.2352941,fgcb:0.2352941,fgca:1,fgde:0.3,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:33563,y:32853,varname:node_3138,prsc:2|emission-6827-OUT;n:type:ShaderForge.SFN_Color,id:7241,x:32426,y:32859,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0.8758622,c3:1,c4:1;n:type:ShaderForge.SFN_Lerp,id:945,x:32801,y:33008,varname:node_945,prsc:2|A-121-RGB,B-7241-RGB,T-1409-V;n:type:ShaderForge.SFN_Color,id:121,x:32426,y:32684,ptovrint:False,ptlb:node_121,ptin:_node_121,varname:node_121,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0,c3:1,c4:1;n:type:ShaderForge.SFN_TexCoord,id:1409,x:32266,y:33022,varname:node_1409,prsc:2,uv:0;n:type:ShaderForge.SFN_ValueProperty,id:4290,x:32523,y:33270,ptovrint:False,ptlb:playerX,ptin:_playerX,varname:node_4290,prsc:2,glob:True,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_RemapRange,id:4070,x:32868,y:33224,varname:node_4070,prsc:2,frmn:-16,frmx:16,tomn:0,tomx:2|IN-9912-OUT;n:type:ShaderForge.SFN_RgbToHsv,id:860,x:32962,y:33031,varname:node_860,prsc:2|IN-945-OUT;n:type:ShaderForge.SFN_HsvToRgb,id:6827,x:33263,y:33087,varname:node_6827,prsc:2|H-9394-OUT,S-860-SOUT,V-860-VOUT;n:type:ShaderForge.SFN_Add,id:9394,x:33119,y:33257,varname:node_9394,prsc:2|A-860-HOUT,B-4070-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1871,x:32523,y:33373,ptovrint:False,ptlb:_playerY,ptin:_playerY,varname:node_1871,prsc:2,glob:True,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Add,id:9912,x:32698,y:33329,varname:node_9912,prsc:2|A-1871-OUT,B-4290-OUT;proporder:7241-121;pass:END;sub:END;*/

Shader "Shader Forge/SlyboxGradient" {
    Properties {
        _Color ("Color", Color) = (0,0.8758622,1,1)
        _node_121 ("node_121", Color) = (0,0,1,1)
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
            uniform float4 _Color;
            uniform float4 _node_121;
            uniform float _playerX;
            uniform float _playerY;
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
                float3 node_945 = lerp(_node_121.rgb,_Color.rgb,i.uv0.g);
                float4 node_860_k = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
                float4 node_860_p = lerp(float4(float4(node_945,0.0).zy, node_860_k.wz), float4(float4(node_945,0.0).yz, node_860_k.xy), step(float4(node_945,0.0).z, float4(node_945,0.0).y));
                float4 node_860_q = lerp(float4(node_860_p.xyw, float4(node_945,0.0).x), float4(float4(node_945,0.0).x, node_860_p.yzx), step(node_860_p.x, float4(node_945,0.0).x));
                float node_860_d = node_860_q.x - min(node_860_q.w, node_860_q.y);
                float node_860_e = 1.0e-10;
                float3 node_860 = float3(abs(node_860_q.z + (node_860_q.w - node_860_q.y) / (6.0 * node_860_d + node_860_e)), node_860_d / (node_860_q.x + node_860_e), node_860_q.x);;
                float3 emissive = (lerp(float3(1,1,1),saturate(3.0*abs(1.0-2.0*frac((node_860.r+((_playerY+_playerX)*0.0625+1.0))+float3(0.0,-1.0/3.0,1.0/3.0)))-1),node_860.g)*node_860.b);
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
