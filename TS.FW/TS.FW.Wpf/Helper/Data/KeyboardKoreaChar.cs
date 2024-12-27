using System;
using System.Linq;

namespace TS.FW.Wpf.Helper.Data
{
    internal class KeyboardKoreaChar
    {
        private static string _chosung = "ㄱㄲㄴㄷㄸㄹㅁㅂㅃㅅㅆㅇㅈㅉㅊㅋㅌㅍㅎ";
        private static string _jungsung = "ㅏㅐㅑㅒㅓㅔㅕㅖㅗㅘㅙㅚㅛㅜㅝㅞㅟㅠㅡㅢㅣ";
        private static string _jongsung = " ㄱㄲㄳㄴㄵㄶㄷㄹㄺㄻㄼㄽㄾㄿㅀㅁㅂㅄㅅㅆㅇㅈㅊㅋㅌㅍㅎ";
        private static string _jongsungDouble = "ㄳㄵㄶㄺㄻㄼㄾㄿㅀㅄ";

        public static ushort _unicodeHangeulBase = 0xAC00;
        public static ushort _unicodehangeullast = 0xD79F;

        public const char Empty = (char)0x20;

        public char Chosung { get; private set; } = Empty;

        public char Jungsung { get; private set; } = Empty;

        public char Jongsung { get; private set; } = Empty;

        public bool IsChosung => _chosung.Contains(this.Chosung);

        public bool IsJungsung => _jungsung.Contains(this.Jungsung);

        public bool IsJongsung => _jongsung.Contains(this.Jongsung);

        public bool IsJongsungDouble => _jongsungDouble.Contains(this.Jongsung);

        public char Contents
        {
            get
            {
                if (this.Chosung != Empty && this.Jungsung != Empty)
                {
                    return this.Merge(this.Chosung, this.Jungsung, this.Jongsung);
                }
                else if (this.Chosung != Empty)
                {
                    return this.Chosung;
                }
                else if (this.Jungsung != Empty)
                {
                    return this.Jungsung;
                }
                else
                {
                    return Empty;
                }
            }
        }

        public KeyboardKoreaChar(char item)
        {
            if (_chosung.Contains(item))
            {
                this.Chosung = item;
            }
            else if (_jungsung.Contains(item))
            {
                this.Jungsung = item;
            }
            else
            {
                this.Divide(item);
            }
        }

        public bool IsJongsungInsert(char chosung)
        {
            return this.Jongsung == Empty && "ㄸㅃㅉ".Contains(chosung) == false;
        }

        public bool IsJongsungComplete(char chosung)
        {
            if (this.Jongsung == 'ㄱ' && "ㅅ".Contains(chosung)) return true;
            else if (this.Jongsung == 'ㄴ' && "ㅈㅎ".Contains(chosung)) return true;
            else if (this.Jongsung == 'ㄹ' && "ㄱㅁㅂㅌㅍㅎ".Contains(chosung)) return true;
            else if (this.Jongsung == 'ㅂ' && "ㅅ".Contains(chosung)) return true;
            else return false;
        }

        public bool IsJungsungDouble(char jungsung)
        {
            if (this.Jungsung == 'ㅗ' && "ㅏㅐㅣ".Contains(jungsung)) return true;
            else if (this.Jungsung == 'ㅜ' && "ㅓㅔㅣ".Contains(jungsung)) return true;
            else if (this.Jungsung == 'ㅡ' && "ㅣ".Contains(jungsung)) return true;
            else return false;
        }

        public char MergeJungsung(char jungsung)
        {
            return this.Merge(this.Chosung, jungsung, Empty);
        }

        public char MergeJongsung(char jongsung)
        {
            return this.Merge(this.Chosung, this.Jungsung, jongsung);
        }

        public char MeargeJungsungDouble(char jungsung)
        {
            if (this.Jungsung == 'ㅗ' && "ㅏㅐㅣ".Contains(jungsung))
            {
                if (jungsung == 'ㅏ') return this.MergeJungsung('ㅘ');
                else if (jungsung == 'ㅐ') return this.MergeJungsung('ㅙ');
                else if (jungsung == 'ㅣ') return this.MergeJungsung('ㅚ');
            }
            else if (this.Jungsung == 'ㅜ' && "ㅓㅔㅣ".Contains(jungsung))
            {
                if (jungsung == 'ㅓ') return this.MergeJungsung('ㅝ');
                else if (jungsung == 'ㅔ') return this.MergeJungsung('ㅞ');
                else if (jungsung == 'ㅣ') return this.MergeJungsung('ㅟ');
            }
            else if (this.Jungsung == 'ㅡ' && "ㅣ".Contains(jungsung))
            {
                if (jungsung == 'ㅣ') return this.MergeJungsung('ㅢ');
            }

            return this.MergeJungsung(this.Jungsung);
        }

        public char MergeJongsungDouble(char chosung)
        {
            if (this.Jongsung == 'ㄱ' && chosung == 'ㅅ')
            {
                return this.MergeJongsung('ㄳ');
            }
            else if (this.Jongsung == 'ㄴ' && "ㅈㅎ".Contains(chosung))
            {
                if (chosung == 'ㅈ') return this.MergeJongsung('ㄵ');
                else if (chosung == 'ㅎ') return this.MergeJongsung('ㄶ');
            }
            else if (this.Jongsung == 'ㄹ' && "ㄱㅁㅂㅅㅌㅍㅎ".Contains(chosung))
            {
                if (chosung == 'ㄱ') return this.MergeJongsung('ㄺ');
                else if (chosung == 'ㅁ') return this.MergeJongsung('ㄻ');
                else if (chosung == 'ㅂ') return this.MergeJongsung('ㄼ');
                else if (chosung == 'ㅅ') return this.MergeJongsung('ㄽ');
                else if (chosung == 'ㅌ') return this.MergeJongsung('ㄾ');
                else if (chosung == 'ㅍ') return this.MergeJongsung('ㄿ');
                else if (chosung == 'ㅎ') return this.MergeJongsung('ㅀ');
            }
            else if (this.Jongsung == 'ㅂ' && chosung == 'ㅅ')
            {
                return this.MergeJongsung('ㅄ');
            }

            return this.MergeJongsung(this.Jongsung);
        }

        public char MergeJongsungJungsung(char jungsung)
        {
            return this.Merge(this.Jongsung, jungsung, Empty);
        }

        public char MergeJongsungDoubleJungsung(char jungsung)
        {
            this.DivideJongsungDouble(out char c1, out char c2);

            return this.Merge(c2, jungsung, Empty);
        }

        public char RemoveJongsung()
        {
            return this.Merge(this.Chosung, this.Jungsung, Empty);
        }

        public char RemoveJongsungDouble()
        {
            this.DivideJongsungDouble(out char c1, out char c2);

            return this.Merge(this.Chosung, this.Jungsung, c1);
        }

        private char Merge(char a, char b, char c)
        {
            var chosungcode = _chosung.IndexOf(a);
            var jungsungcode = _jungsung.IndexOf(b);
            var jongsungcode = _jongsung.IndexOf(c);

            return Convert.ToChar(_unicodeHangeulBase + (chosungcode * 21 + jungsungcode) * 28 + jongsungcode);
        }

        private void Divide(char c)
        {
            var check = Convert.ToUInt16(c);

            if (check > _unicodehangeullast || check < _unicodeHangeulBase) return;

            var code = check - _unicodeHangeulBase;

            var jongsungcode = code % 28;
            code = (code - jongsungcode) / 28;

            var jungsungcode = code % 21;
            code = (code - jungsungcode) / 21;

            var chosungCode = code;

            this.Chosung = _chosung[chosungCode];
            this.Jungsung = _jungsung[jungsungcode];
            this.Jongsung = _jongsung[jongsungcode];
        }

        private void DivideJongsungDouble(out char chosung1, out char chosung2)
        {
            chosung1 = Empty;
            chosung2 = Empty;

            switch (this.Jongsung)
            {
                case 'ㄳ':
                    {
                        chosung1 = 'ㄱ';
                        chosung2 = 'ㅅ';
                    }
                    break;
                case 'ㄵ':
                    {
                        chosung1 = 'ㄴ';
                        chosung2 = 'ㅈ';
                    }
                    break;
                case 'ㄶ':
                    {
                        chosung1 = 'ㄴ';
                        chosung2 = 'ㅎ';
                    }
                    break;
                case 'ㄺ':
                    {
                        chosung1 = 'ㄹ';
                        chosung2 = 'ㄱ';
                    }
                    break;
                case 'ㄻ':
                    {
                        chosung1 = 'ㄹ';
                        chosung2 = 'ㅁ';
                    }
                    break;
                case 'ㄼ':
                    {
                        chosung1 = 'ㄹ';
                        chosung2 = 'ㅂ';
                    }
                    break;
                case 'ㄾ':
                    {
                        chosung1 = 'ㄹ';
                        chosung2 = 'ㅌ';
                    }
                    break;
                case 'ㄿ':
                    {
                        chosung1 = 'ㄹ';
                        chosung2 = 'ㅍ';
                    }
                    break;
                case 'ㅀ':
                    {
                        chosung1 = 'ㄹ';
                        chosung2 = 'ㅎ';
                    }
                    break;
                case 'ㅄ':
                    {
                        chosung1 = 'ㅂ';
                        chosung2 = 'ㅅ';
                    }
                    break;
            }
        }

        public override string ToString()
        {
            return this.Contents.ToString();
        }

        public static implicit operator KeyboardKoreaChar(char item)
        {
            return new KeyboardKoreaChar(item);
        }

        public static implicit operator char(KeyboardKoreaChar item)
        {
            return item.Contents;
        }
    }
}
