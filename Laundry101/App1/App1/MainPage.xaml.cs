using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Text.RegularExpressions;

namespace App1
{
    public partial class MainPage : ContentPage
    {
        Dictionary<int, string> DicEntryName = new Dictionary<int, string>();

        int InputLen = 2;

        //*******************************************************************************
        public MainPage()
        {
            InitializeComponent();

        }

        //*******************************************************************************
        public bool Check_Entry()
        {
            bool blPrice = false;
            bool blCapa = false;
            bool blUsage = false;

            if (EntryPrice.Text.Length > 0) blPrice = true;
            if (EntryCapa.Text.Length > 0) blCapa = true;
            if (EntryUsage.Text.Length > 0) blUsage = true;

            return blPrice & blCapa & blUsage;

        }

        public bool CheckInput(Entry entry)
        {
            Regex regex = new Regex("[^0-9]+");
            bool blResult = false;
            string inputText = entry.Text.Substring(entry.Text.Length - 1, 1);

            //入力文字の削除フラグをセット
            if (regex.IsMatch(inputText)) blResult = true;
            else if (entry.Text.Length > InputLen) blResult = true;

            return blResult;
        }

        //*******************************************************************************
        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            int iHashCode_EntryPrice = EntryPrice.GetHashCode();
            int iHashCode_EntryCapa = EntryCapa.GetHashCode();
            int iHashCode_EntryUsage = EntryUsage.GetHashCode();

            //BackSpaseやDelete用の対策
            if (e.NewTextValue.Length <= 0) return;

            if (sender.GetHashCode() == iHashCode_EntryPrice && CheckInput(EntryPrice))
                EntryPrice.Text = EntryPrice.Text.Remove(EntryPrice.Text.Length - 1, 1);

            else if (sender.GetHashCode() == iHashCode_EntryCapa && CheckInput(EntryCapa))
                EntryCapa.Text = EntryCapa.Text.Remove(EntryCapa.Text.Length - 1, 1);

            else if (sender.GetHashCode() == iHashCode_EntryUsage && CheckInput(EntryUsage))
                EntryUsage.Text = EntryUsage.Text.Remove(EntryUsage.Text.Length - 1, 1);
        }

        //*******************************************************************************
        private void BtnCalculation_Clicked(object sender, EventArgs e)
        {

        }



    }
}


//*******************************************************************************
//*******************************************************************************
