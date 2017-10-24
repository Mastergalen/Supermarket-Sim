// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.04 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.04;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:0,bsrc:0,bdst:1,culm:0,dpts:2,wrdp:True,dith:2,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:6715,x:32928,y:32504,varname:node_6715,prsc:2|emission-3800-RGB;n:type:ShaderForge.SFN_Tex2d,id:3800,x:32668,y:32674,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_MainTex,prsc:2,tex:ba526ec6a44e73442a62f441ca1a684f,ntxv:0,isnm:False|UVIN-9926-OUT;n:type:ShaderForge.SFN_Time,id:3380,x:31447,y:33070,varname:node_3380,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:8825,x:31770,y:32768,ptovrint:False,ptlb:numU,ptin:_numU,varname:_numU,prsc:2,glob:False,v1:4;n:type:ShaderForge.SFN_ValueProperty,id:2969,x:31770,y:32893,ptovrint:False,ptlb:numV,ptin:_numV,varname:_numV,prsc:2,glob:False,v1:8;n:type:ShaderForge.SFN_Divide,id:858,x:31957,y:32636,varname:node_858,prsc:2|A-3376-OUT,B-8825-OUT;n:type:ShaderForge.SFN_Vector1,id:3376,x:31770,y:32636,varname:node_3376,prsc:2,v1:1;n:type:ShaderForge.SFN_Divide,id:139,x:31957,y:32783,varname:node_139,prsc:2|A-3376-OUT,B-2969-OUT;n:type:ShaderForge.SFN_Append,id:5778,x:32142,y:32635,varname:node_5778,prsc:2|A-858-OUT,B-139-OUT;n:type:ShaderForge.SFN_TexCoord,id:9771,x:32142,y:32783,varname:node_9771,prsc:2,uv:0;n:type:ShaderForge.SFN_Multiply,id:3285,x:32321,y:32691,varname:node_3285,prsc:2|A-5778-OUT,B-9771-UVOUT;n:type:ShaderForge.SFN_Add,id:9926,x:32488,y:32691,varname:node_9926,prsc:2|A-3285-OUT,B-3731-OUT;n:type:ShaderForge.SFN_Code,id:8633,x:32116,y:33037,varname:node_8633,prsc:2,code:ZgBsAG8AYQB0ACAAbwBuAGUAQwBvAGwAIAA9ACAAMQAvAG4AdQBtAEMAbwBsADsACgBmAGwAbwBhAHQAIABhAGwAbABTAHQAZQBwAHMAIAA9ACAAMQAvACgAbgB1AG0AQwBvAGwAKgBuAHUAbQBSAG8AdwApADsACgBpAG4AdAAgAGMAdQByAGUAbgB0AEMAbwBsACAAPQAgAGYAbABvAG8AcgAoAFQAaQBtAGUALwBhAGwAbABTAHQAZQBwAHMAKQA7AAoACgAKAGkAZgAoAGMAdQByAGUAbgB0AEMAbwBsACAAPgA9ACAAbgB1AG0AQwBvAGwAKQAKAHsACgBjAHUAcgBlAG4AdABDAG8AbAAgAD0AIABjAHUAcgBlAG4AdABDAG8AbAAgAC0AIAAoACgAZgBsAG8AbwByACgAYwB1AHIAZQBuAHQAQwBvAGwAIAAvACAAbgB1AG0AQwBvAGwAKQApACAAKgAgAG4AdQBtAEMAbwBsACkAOwAKAH0ACgAKAHIAZQB0AHUAcgBuACAAKABvAG4AZQBDAG8AbAAgACoAIABjAHUAcgBlAG4AdABDAG8AbAApADsA,output:0,fname:Function_node_1253,width:656,height:175,input:0,input:0,input:0,input_1_label:numCol,input_2_label:Time,input_3_label:numRow|A-8825-OUT,B-191-OUT,C-2969-OUT;n:type:ShaderForge.SFN_Frac,id:191,x:31813,y:33141,varname:node_191,prsc:2|IN-9031-OUT;n:type:ShaderForge.SFN_Append,id:3731,x:32840,y:33151,varname:node_3731,prsc:2|A-8633-OUT,B-9395-OUT;n:type:ShaderForge.SFN_Code,id:9395,x:32117,y:33250,varname:node_9395,prsc:2,code:ZgBsAG8AYQB0ACAAbwBuAGUAUgBvAHcAIAA9ACAAMQAvAG4AdQBtAFIAbwB3ADsACgBpAG4AdAAgAGMAdQByAGUAbgB0AFIAbwB3ACAAPQAgACgAVABpAG0AZQAgAC8AIABvAG4AZQBSAG8AdwApADsACgByAGUAdAB1AHIAbgAgACgAMQAgAC0AIAAoAG8AbgBlAFIAbwB3ACoAYwB1AHIAZQBuAHQAUgBvAHcAKQApACAALQAgAG4AdQBtAFIAbwB3ADsA,output:0,fname:Function_node_4725,width:655,height:167,input:0,input:0,input_1_label:numRow,input_2_label:Time|A-2969-OUT,B-191-OUT;n:type:ShaderForge.SFN_Multiply,id:9031,x:31646,y:33141,varname:node_9031,prsc:2|A-3380-T,B-5412-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5412,x:31447,y:33269,ptovrint:False,ptlb:Speed,ptin:_Speed,varname:_Speed,prsc:2,glob:False,v1:0.3;proporder:3800-8825-2969-5412;pass:END;sub:END;*/

Shader "Supermarket/sh_supermarket_advertising_anim_01" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _numU ("numU", Float ) = 4
        _numV ("numV", Float ) = 8
        _Speed ("Speed", Float ) = 0.3
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _numU;
            uniform float _numV;
            float Function_node_1253( float numCol , float Time , float numRow ){
            float oneCol = 1/numCol;
            float allSteps = 1/(numCol*numRow);
            int curentCol = floor(Time/allSteps);
            
            
            if(curentCol >= numCol)
            {
            curentCol = curentCol - ((floor(curentCol / numCol)) * numCol);
            }
            
            return (oneCol * curentCol);
            }
            
            float Function_node_4725( float numRow , float Time ){
            float oneRow = 1/numRow;
            int curentRow = (Time / oneRow);
            return (1 - (oneRow*curentRow)) - numRow;
            }
            
            uniform float _Speed;
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
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
/////// Vectors:
////// Lighting:
////// Emissive:
                float node_3376 = 1.0;
                float4 node_3380 = _Time + _TimeEditor;
                float node_191 = frac((node_3380.g*_Speed));
                float2 node_9926 = ((float2((node_3376/_numU),(node_3376/_numV))*i.uv0)+float2(Function_node_1253( _numU , node_191 , _numV ),Function_node_4725( _numV , node_191 )));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_9926, _MainTex));
                float3 emissive = _MainTex_var.rgb;
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
