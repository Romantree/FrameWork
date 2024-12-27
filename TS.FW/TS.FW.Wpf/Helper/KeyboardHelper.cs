using System;
using System.Collections.Generic;
using TS.FW.Wpf.Helper.Data;

namespace TS.FW.Wpf.Helper
{
    internal class KeyboardHelper
    {
        private Stack<string> _undoList = new Stack<string>();
        private string _text = string.Empty;

        private char _LastChar => this._text.Length <= 0 ? (char)0 : this._text[this._text.Length - 1];

        public string Text
        {
            get
            {
                return this._text;
            }
            set
            {
                this._undoList.Push(this._text);
                this._text = value;

                this.NotifyOnTextChangedEvent();
            }
        }

        public event EventHandler<string> OnTextChanged;

        public KeyboardHelper()
        {

        }

        public void SetSpace()
        {
            try
            {
                this.Text += " ";
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        public void SetBackSpace()
        {
            try
            {
                if (this._undoList.Count <= 0) return;

                this._text = this._undoList.Pop();
                this.NotifyOnTextChangedEvent();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        public void SetContents(string contents)
        {
            try
            {
                this.Text += contents;
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        public void SetContents(char contents)
        {
            try
            {
                if (char.GetUnicodeCategory(contents) == System.Globalization.UnicodeCategory.OtherLetter)
                {
                    this.SetContents(contents, this._LastChar);
                }
                else
                {
                    this.Text += contents;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void SetContents(KeyboardKoreaChar contents, KeyboardKoreaChar lastContents)
        {
            if (contents.IsChosung)
            {
                if (lastContents.IsChosung && lastContents.IsJungsung && lastContents.IsJongsungComplete(contents.Chosung))
                {
                    this.SaveText(lastContents.MergeJongsungDouble(contents.Chosung));
                }
                else if (lastContents.IsChosung && lastContents.IsJungsung && lastContents.IsJongsungInsert(contents.Chosung))
                {
                    this.SaveText(lastContents.MergeJongsung(contents.Chosung));
                }
                else
                {
                    this.Text += contents;
                }
            }
            else if (contents.IsJungsung)
            {
                if (lastContents.Jongsung != KeyboardKoreaChar.Empty)
                {
                    if (lastContents.IsJongsungDouble)
                    {
                        this.SaveText(lastContents.RemoveJongsungDouble(), lastContents.MergeJongsungDoubleJungsung(contents.Jungsung));
                    }
                    else
                    {
                        this.SaveText(lastContents.RemoveJongsung(), lastContents.MergeJongsungJungsung(contents.Jungsung));
                    }
                }
                else if (lastContents.IsChosung && lastContents.IsJungsung == false)
                {
                    this.SaveText(lastContents.MergeJungsung(contents.Jungsung));
                }
                else if (lastContents.IsChosung && lastContents.IsJungsungDouble(contents.Jungsung))
                {
                    this.SaveText(lastContents.MeargeJungsungDouble(contents.Jungsung));
                }
                else
                {
                    this.Text += contents;
                }
            }
        }

        private void SaveText(char contents)
        {
            this.Text = this.Text.Remove(this.Text.Length - 1);

            this._text += contents;
            this.NotifyOnTextChangedEvent();
        }

        private void SaveText(char contents1, char contents2)
        {
            this.Text = this.Text.Remove(this.Text.Length - 1);

            this._text += string.Format("{0}{1}", contents1, contents2);
            this.NotifyOnTextChangedEvent();
        }

        private void NotifyOnTextChangedEvent()
        {
            this.OnTextChanged?.Invoke(this, this._text);
        }
    }
}
