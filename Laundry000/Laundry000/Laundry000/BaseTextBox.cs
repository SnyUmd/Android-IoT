using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Text.RegularExpressions;

namespace Laundry000
{
    [System.Diagnostics.DebuggerStepThrough()]
    public class BaseTextBox : Entry
    {
        public BaseTextBox() : base()
        {
            this.TextChanged += (sender, e) =>
            {
                string value = e.NewTextValue;

                //入力文字数制限
                if (MaxLength > 0 &&
                    value.Length > MaxLength)
                {
                    //入力文字数の制限値を超えた場合は、制限値までの文字列までを表示する
                    value = value.Substring(0, MaxLength);
                }

                //入力フォーマット
                if (!String.IsNullOrEmpty(this.Format))
                {
                    //数値以外の文字と+-.以外の記号を取り除きます。
                    //（カンマもとりあえず取り除きます）
                    value = Regex.Replace(value, @"[^\d-+.]", "");

                    //フォーマットを指定して文字列を成形します。
                    decimal ret = Decimal.Zero;
                    if (Decimal.TryParse(value, out ret))
                    {
                        value = ret.ToString(this.Format);
                    }
                }

                //余計なTextChangedイベントを発生させないため、最後に１行のみ値を戻す処理を行う
                if (value != e.NewTextValue)
                {
                    this.Text = value;
                }
            };
        }

        public int MaxLength { get; set; }
        public string Format { get; set; }
    }
}

//使い方
/*
<base:BaseTextBox x:Name="txtPrice"
                      Text=""
                      Keyboard="Numeric"
                      MaxLength="5"
                      Format="#,##0"
                      HorizontalTextAlignment="End"
                      HorizontalOptions="End" />    <!--Format:N0/C0/###0-->
*/