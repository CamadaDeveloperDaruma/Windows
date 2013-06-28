using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarumaMobileFramework
{
    public class D_HTMLParser
    {
        private enum TagType
        {
            //Tags de Formatação
            b,  i,  ad, s,  e,  c,  
            l,  sl, ce, dt, hr, n,
            tb, da, xl, fe, slm, 

            //Tags de Comando, ibmp nao implementado
            sn, g, gui, ft,

            //Tags de Código de barras
            pdf, ean13, 

            //Tag de texto literal (interna)
            parser_literal

        }

        private struct TagStruct
        {
            public TagType Tag;
            public int ParamsStart;
            public string Params;
        }

        Stack<TagStruct> _tagStack = new Stack<TagStruct>();
        StringBuilder _sb;
        
        public string Parse(string text)
        {
            _sb = new StringBuilder(text.Length*2);
            return ParseString(ref text);
        }

        private string ParseString(ref string text) {
            int posTagStart = 0;
            int posTagEnd = 0;
            int posLastData = 0;
            bool lastTagHasParams = false;

            while (posTagStart != -1)
            {
                //procura abertura de tag
                posTagStart = text.IndexOf('<', posTagStart);

                if (posTagStart == -1)
                {
                    if (posLastData < text.Length - 1)
                    {
                        string fimstr = text.Substring(posLastData, text.Length - posLastData);
                        _sb.Append(fimstr);
                    }
                    break;
                }
                //procura fechamento da tag
                posTagEnd = text.IndexOf('>', posTagStart + 1);
                //verifica se nao ha nenhum caracter < antes do >
                int posNextTagStart = text.IndexOf('<', posTagStart + 1);
                if ((posNextTagStart != -1) && (posNextTagStart < posTagEnd))
                {
                    //temos um literal, reiniciar a busca
                    posTagStart = posNextTagStart;
                    continue;
                }

                if (!lastTagHasParams)
                {
                    //temos uma tag, copia substring
                    _sb.Append(text.Substring(posLastData, posTagStart - posLastData));
                }
                posTagStart = HaveTag(ref text, posTagStart, posTagEnd, out lastTagHasParams);
                posLastData = posTagStart;
            }

            _sb.Append('\n');
            return _sb.ToString();
        }

        private int HaveTag(ref string text, int start, int end, out bool tagHasParams)
        {
            tagHasParams = false;
            if (text[start + 1] == '/') //fecha tag
            {
                return CloseTag(ref text, start, end);
            }
            
            string tagName = text.Substring(start+1, end - start - 1).ToLower();
            
            TagStruct t = new TagStruct();
            t.Tag = TagNameToTagType(tagName);
            string tagCmd = TagToString(t, out tagHasParams);

            if (tagHasParams) t.ParamsStart = end + 1;
            else t.ParamsStart = -1;
            _sb.Append(tagCmd);
            _tagStack.Push(t);

            return end + 1;
        }

        private int CloseTag(ref string text, int start, int end)
        {
            string tagName = text.Substring(start + 2, end - start - 2).ToLower();
            TagStruct stackTag = _tagStack.Peek();

            if (stackTag.Tag == TagNameToTagType(tagName))
            {
                if (stackTag.ParamsStart != -1) //copia parametros
                {
                    stackTag.Params = text.Substring(stackTag.ParamsStart, start - stackTag.ParamsStart);
                }
                _sb.Append(TagToString(stackTag));
                _tagStack.Pop();
                return end + 1;
            }
            else return -1;
        }

        private TagType TagNameToTagType(string tagName)
        {
            if (tagName == "b") return TagType.b;
            else if (tagName == "i") return TagType.i;
            #region vários else ifs
            else if (tagName == "ad") return TagType.ad;
            else if (tagName == "s") return TagType.s;
            else if (tagName == "e") return TagType.e;
            else if (tagName == "c") return TagType.c;
            else if (tagName == "n") return TagType.n;
            else if (tagName == "l") return TagType.l;
            else if (tagName == "sl") return TagType.sl;
            else if (tagName == "ce") return TagType.ce;
            else if (tagName == "dt") return TagType.dt;
            else if (tagName == "hr") return TagType.hr;
            else if (tagName == "tb") return TagType.tb;
            else if (tagName == "da") return TagType.da;
            else if (tagName == "xl") return TagType.xl;
            else if (tagName == "fe") return TagType.fe;
            else if (tagName == "slm") return TagType.slm;
            else if (tagName == "sn") return TagType.sn;
            else if (tagName == "g") return TagType.g;
            else if (tagName == "gui") return TagType.gui;
            else if (tagName == "pdf") return TagType.pdf;
            else if (tagName == "ft") return TagType.ft;
            else if (tagName == "ean13") return TagType.ean13;
            #endregion
            else return TagType.parser_literal; //nao eh uma tag valida, tratar como literal 
        }


        private string TagToString(TagStruct tag)
        {
            bool x;
            return TagToString(tag, out x);
        }

        private string TagToString(TagStruct tag, out bool tagHasParams)
        {
            tagHasParams = false;
            if (!LastTagEquals(tag.Tag)) //nova tag
            {
                switch (tag.Tag)
                {
                    case TagType.b: //Negrito
                        return "\x001bE"; //ESC E
                    case TagType.i: //Italico
                        return "\x001b\x0034\x31"; //ESC 4 1
                    case TagType.ad: //Alinha a Direita
                        return "\x001b\x006a\x0032";
                    case TagType.s: //Sublinhado
                        return "\x001b\x002d\x0001"; //ESC 0x2D 0x1
                    case TagType.e: //Expandido
                        return "\x000e";
                    case TagType.c: //Modo Condensado
                        return "\x000f";
                    case TagType.l:
                        return "\n\n";
                    case TagType.sl:
                        tagHasParams = true;
                        break;
                    case TagType.ce:
                        return "\x001b\x006a\x0031"; //ESC j 1
                    case TagType.dt:
                        return DateTime.Now.ToString("dd/MM/yy");
                    case TagType.hr:
                        return DateTime.Now.ToString("HH:mm:ss");
                    case TagType.n:
                        return "\x0014";
                    case TagType.tb:
                        break;
                    case TagType.da: //Dupla Altura
                        return "\x001b\x0077\x0031";
                    case TagType.xl://Extra Grande
                        return "\x001b\x0079\x0031";
                    case TagType.fe:
                        return "\x001b\x0021\x0001";
                    case TagType.slm:
                        break;
                    case TagType.sn:
                        tagHasParams = true;
                        break;
                    case TagType.g:
                        return "\x001b\x0070";
                    case TagType.gui:
                        return "\x001b\x006d";
                    case TagType.ft:
                        break;
                    case TagType.pdf:
                        break;
                    case TagType.ean13:
                        return "\x001bb\x0001\x0000\x0000\x0000";
                }
            }
            else
            {
                switch (tag.Tag)
                {
                    case TagType.b:
                        return "\x001bF"; //ESC E
                    case TagType.i:
                        return "\x001b\x0034\x0030"; //ESC 4 0
                    case TagType.ad: //Fim Alinha Direita = Alinha Esq
                        return "\x001b\x006a\x0030";
                    case TagType.s:
                        return "\x001b\x002d\x0000";
                    case TagType.e:
                        return "\x0014";
                    case TagType.c:
                        return "\x0012";
                    case TagType.sl:
                        int numLinhas = Int32.Parse(tag.Params);
                        StringBuilder sb = new StringBuilder(numLinhas + 1);
                        sb.Append('\n', numLinhas + 1);
                        return sb.ToString();
                    case TagType.ce: //Fim Centralizar = Alinha Esq
                        return "\x001b\x006a\x0030";
                    case TagType.n:
                        break;
                    case TagType.tb:
                        break;
                    case TagType.da:
                        return "\x001b\x0077\x0030";
                    case TagType.xl:
                        return "\x001b\x0079\x0030";
                    case TagType.fe:
                        return "\x001b\x0021\x0000";
                    case TagType.slm:
                        break;
                    case TagType.sn:
                        int numBeeps = Int32.Parse(tag.Params);
                        StringBuilder sbbeep = new StringBuilder(numBeeps);
                        sbbeep.Append('\x0007', numBeeps);
                        return sbbeep.ToString();
                    case TagType.ft:
                        break;
                    case TagType.pdf:
                        break;
                    case TagType.ean13:
                        return "\x0000";
                }
            }
            return "";
        }

        private bool LastTagEquals(TagType tag)
        {
            try
            {
                TagStruct lastTag = _tagStack.Peek();
                if (lastTag.Tag == tag) return true;
            }
            catch
            {
                return false;
            }
            
            return false;
        }
    }
}
