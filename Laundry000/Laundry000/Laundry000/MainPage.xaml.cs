using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Text.RegularExpressions;

namespace Laundry000
{
    public partial class MainPage : ContentPage
    {
        public string Frmat { get; set; }
        Dictionary<int, string> DicEntryName = new Dictionary<int, string>();

        int MaxLen = 6;

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


            if (EntryPrice.Text != null && EntryPrice.Text.Length > 0) blPrice = true;
            if (EntryCapa.Text != null && EntryCapa.Text.Length > 0) blCapa = true;
            if (EntryUsage.Text != null && EntryUsage.Text.Length > 0) blUsage = true;

            return blPrice & blCapa & blUsage;
        }

        //*******************************************************************************
        public string CheckText(string str)
        {
            string strRtn;
            string strRemove = @"[^\d-+.]";

            strRtn = str;

            if (strRtn.Length > MaxLen)
                strRtn = strRtn.Substring(0, MaxLen);

            strRtn = Regex.Replace(strRtn, strRemove, "");

            return strRtn;
        }


        //*******************************************************************************
        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            int iHashCode_EntryPrice = EntryPrice.GetHashCode();
            int iHashCode_EntryCapa = EntryCapa.GetHashCode();
            int iHashCode_EntryUsage = EntryUsage.GetHashCode();

            //BackSpaseやDelete用の対策
            if (e.NewTextValue.Length <= 0)
            {
                if (sender.GetHashCode() == iHashCode_EntryPrice)
                    EntryPrice.Text = CheckText(EntryPrice.Text);

                else if (sender.GetHashCode() == iHashCode_EntryCapa)
                    EntryCapa.Text = CheckText(EntryCapa.Text);

                else if (sender.GetHashCode() == iHashCode_EntryUsage)
                    EntryUsage.Text = CheckText(EntryUsage.Text);
            }
            BtnCalculation.IsEnabled = Check_Entry();
        }

        //*******************************************************************************
        private void BtnCalculation_Clicked(object sender, EventArgs e)
        {
            int iPrice = int.Parse(EntryPrice.Text);
            int iCapa = int.Parse(EntryCapa.Text);
            int iUsage = int.Parse(EntryUsage.Text);

            long loTotalUse = iCapa / iUsage;
            long loOneUsePrice = iPrice / loTotalUse;
            int iAddCycle = 1000 / iUsage;

            Label_PriceParTime.Text = loOneUsePrice.ToString();
            Label_NumberOfUses.Text = loTotalUse.ToString();
            Label_ReplenishmentCycle.Text = iAddCycle.ToString();

            //BtnCopy.IsEnabled = true;

        }

        private void BtnCopy_Clicked(object sender, EventArgs e)
        {

        }
    }
}
