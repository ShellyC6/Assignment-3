using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;//FileStream, Reader/Writer classes
using System.Runtime.Serialization; //IFormatter
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using RealEstateBLL;
using RealEstateDAL;

namespace RealEstateApp
{
    public partial class Form3 : Form
    {
        Estate EstateObj = null;
        EstateManager EManagerObj = new EstateManager();
        bool change = false;
        string fileName = String.Empty;
        public Form3()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            // Display the enum countries in a listbox
            Array array = Enum.GetValues(Typeof(RealEstateBLL.Countries));
            listBox_Countries.BeginUpdate();
            foreach (object obj in array)
            {
                listBox_Countries.Items.Add(obj);
            }
            // Initialize the current estate textBox
            // DisplayEstate();
            DisplayListEstate();
        }

        /// <summary>
        /// Clear every listBox, checkBox and textBox of the form and hide some groupBox
        /// </summary>
        
        private void ClearForm()
        {
            // Hide the specific groupBoxes
            HideAllEstateFields();
            // Unselect all listBoxes
            listBox_category.ClearSelected();
            listBox_residential.ClearSelected();
            listBox_commercial.ClearSelected();
            listBox_institutional.ClearSelected();
            listBox_Countries.ClearSelected();
            listBox_LegalForm.ClearSelected();
            listBox_PaymentSystem.ClearSelected();
            // Hide unecessary listBoxes and groupBoxes
            listBox_commercial.Visible = false;
            listBox_institutional.Visible = false;
            listBox_residential.Visible = false;
            ResidentialFields.Visible = false;
            InstitutionalFields.Visible = false;
            CommercialFields.Visible = false;
            groupBox_Bank.Visible = false;
            groupBox_WesternUnion.Visible = false;
            groupBox_PayPal.Visible = false;
            // Delete the text in all textBoxes
            textBox_City.Clear();
            textBox_CurrentEstate.Clear();
            textBox_GardenSize.Clear();
            textBox_ID.Clear();
            textBox_NbClassrooms.Clear();
            textBox_NbFaculties.Clear();
            textBox_NbFloors.Clear();
            textBox_NbRooms.Clear();
            textBox_NbSeats.Clear();
            textBox_NbShelfs.Clear();
            textBox_Street.Clear();
            textBox_VitrineLength.Clear();
            textBox_ZipCode.Clear();
            textBox_AccountNumber.Clear();
            textBox_Amount.Clear();
            textBox_Email.Clear();
            textBox_EmailPayPal.Clear();
            textBox_Name.Clear();
            textBox_NameWU.Clear();
            textBox_Options.Clear();
            // Uncheck all checkboxes
            checkBox_Balcony.Checked = false;
            checkBox_Library.Checked = false;
            checkBox_Pool.Checked = false;
            // Clear the pictureBox
            pictureBox.Image = null;
            // Display the current estate if there is one
            //DisplayEstate();

            DisplayListEstate();
        }

        private void HideAllEstateFields()
        {
            groupBox_Villa.Visible = false;
            groupBox_Apartment.Visible = false;
            groupBox_Townhouse.Visible = false;
            groupBox_Shop.Visible = false;
            groupBox_Warehouse.Visible = false;
            groupBox_School.Visible = false;
            groupBox_University.Visible = false;
        }

        /// <summary>
        /// Check every necessary field to know if we can create the estate
        /// Return true if we can create the estate, false if we can't
        /// </summary>
        private bool ValidateInput()
        {
            int number;

            // Check if a category is selected and if the fields of this category are well filled
            int SelectedCategory = listBox_category.SelectedIndex;
            switch (SelectedCategory)
            {
                case 0:
                    if (Int32.TryParse(textBox_NbRooms.Text, out number) == false) return false;
                    if (number <= 0) return false;
                    break;
                case 1:
                    if (Int32.TryParse(textBox_NbFloors.Text, out number) == false) return false;
                    if (number <= 0) return false;
                    break;
                case 2:
                    if (Int32.TryParse(textBox_NbSeats.Text, out number) == false) return false;
                    if (number < 0) return false;
                    break;
                default:
                    return false;
            }

            // Check if a Type is selected and if the fields of this Type are well filled
            RealEstateBLL.Type SelectedType = GetSelectedType();
            switch (SelectedType)
            {
                case RealEstateBLL.Type.villa:
                    break;
                case RealEstateBLL.Type.apartment:
                    break;
                case RealEstateBLL.Type.townhouse:
                    if (Int32.TryParse(textBox_GardenSize.Text, out number) == false) return false;
                    if (number < 0) return false;
                    break;
                case RealEstateBLL.Type.shop:
                    if (Int32.TryParse(textBox_VitrineLength.Text, out number) == false) return false;
                    if (number < 0) return false;
                    break;
                case RealEstateBLL.Type.warehouse:
                    if (Int32.TryParse(textBox_NbShelfs.Text, out number) == false) return false;
                    if (number <= 0) return false;
                    break;
                case RealEstateBLL.Type.school:
                    if (Int32.TryParse(textBox_NbClassrooms.Text, out number) == false) return false;
                    if (number <= 0) return false;
                    break;
                case RealEstateBLL.Type.university:
                    if (Int32.TryParse(textBox_NbFaculties.Text, out number) == false) return false;
                    if (number <= 0) return false;
                    break;
                default:
                    return false;
            }

            // Check if a payment system is selected and if the fields of this payment system are well filled
            int SelectedPayment = listBox_PaymentSystem.SelectedIndex;
            switch (SelectedPayment)
            {
                case 0:
                    if (Int32.TryParse(textBox_AccountNumber.Text, out number) == false) return false;
                    if (textBox_Name.Text == "") return false;
                    break;
                case 1:
                    if (textBox_NameWU.Text == "") return false;
                    if (textBox_Email.Text == "") return false;
                    break;
                case 2:
                    if (textBox_EmailPayPal.Text == "") return false;
                    break;
                default:
                    return false;
            }
            if (textBox_Amount.Text == "") return false;

            // Check the fields of the adress and legal form
            if (textBox_Street.Text == "") return false;
            if (Int32.TryParse(textBox_ZipCode.Text, out number) == false) return false;
            if (number <= 0) return false;
            if (textBox_City.Text == "") return false;
            if (listBox_Countries.SelectedIndex < 0) return false;
            if (Int32.TryParse(textBox_ID.Text, out number) == false) return false;
            if (number <= 0) return false;
            if (listBox_LegalForm.SelectedIndex < 0) return false;

            // Return true if all the fields are well filled
            return true;
        }

        private void AddEstate()
        {
            EstateObj = null;


            // Create the payment system selected
            Payment PaymentSystem;
            int SelectedPayment = listBox_PaymentSystem.SelectedIndex;
            switch (SelectedPayment)
            {
                case 0:
                    PaymentSystem = new Bank(int.Parse(textBox_Amount.Text), textBox_Options.Text, textBox_Name.Text, int.Parse(textBox_AccountNumber.Text));
                    break;
                case 1:
                    PaymentSystem = new WesternUnion(int.Parse(textBox_Amount.Text), textBox_Options.Text, textBox_NameWU.Text, textBox_Email.Text);
                    break;
                case 2:
                    PaymentSystem = new PayPal(int.Parse(textBox_Amount.Text), textBox_Options.Text, textBox_EmailPayPal.Text);
                    break;
                default:
                    PaymentSystem = null;
                    break;
            }

            // Create the estate
            RealEstateBLL.Type SelectedType = GetSelectedType();
            switch (SelectedType)
            {
                case RealEstateBLL.Type.villa:
                    EstateObj = new Villa(int.Parse(textBox_ID.Text), new Address(int.Parse(textBox_ZipCode.Text), textBox_Street.Text, listBox_Countries.SelectedIndex, textBox_City.Text), 0, GetSelectedLegalForm(), PaymentSystem, int.Parse(textBox_NbRooms.Text), checkBox_Pool.Checked);
                    break;
                case RealEstateBLL.Type.apartment:
                    EstateObj = new Apartment(int.Parse(textBox_ID.Text), new Address(int.Parse(textBox_ZipCode.Text), textBox_Street.Text, listBox_Countries.SelectedIndex, textBox_City.Text), 0, GetSelectedLegalForm(), PaymentSystem, int.Parse(textBox_NbRooms.Text), checkBox_Balcony.Checked);
                    break;
                case RealEstateBLL.Type.townhouse:
                    EstateObj = new Townhouse(int.Parse(textBox_ID.Text), new Address(int.Parse(textBox_ZipCode.Text), textBox_Street.Text, listBox_Countries.SelectedIndex, textBox_City.Text), 0, GetSelectedLegalForm(), PaymentSystem, int.Parse(textBox_NbRooms.Text), int.Parse(textBox_GardenSize.Text));
                    break;
                case RealEstateBLL.Type.shop:
                    EstateObj = new Shop(int.Parse(textBox_ID.Text), new Address(int.Parse(textBox_ZipCode.Text), textBox_Street.Text, listBox_Countries.SelectedIndex, textBox_City.Text), 0, GetSelectedLegalForm(), PaymentSystem, int.Parse(textBox_NbFloors.Text), int.Parse(textBox_VitrineLength.Text));
                    break;
                case RealEstateBLL.Type.warehouse:
                    EstateObj = new Warehouse(int.Parse(textBox_ID.Text), new Address(int.Parse(textBox_ZipCode.Text), textBox_Street.Text, listBox_Countries.SelectedIndex, textBox_City.Text), 0, GetSelectedLegalForm(), PaymentSystem, int.Parse(textBox_NbFloors.Text), int.Parse(textBox_NbShelfs.Text));
                    break;
                case RealEstateBLL.Type.school:
                    EstateObj = new School(int.Parse(textBox_ID.Text), new Address(int.Parse(textBox_ZipCode.Text), textBox_Street.Text, listBox_Countries.SelectedIndex, textBox_City.Text), 0, GetSelectedLegalForm(), PaymentSystem, checkBox_Library.Checked, int.Parse(textBox_NbSeats.Text), int.Parse(textBox_NbClassrooms.Text));
                    break;
                case RealEstateBLL.Type.university:
                    EstateObj = new University(int.Parse(textBox_ID.Text), new Address(int.Parse(textBox_ZipCode.Text), textBox_Street.Text, listBox_Countries.SelectedIndex, textBox_City.Text), 0, GetSelectedLegalForm(), PaymentSystem, checkBox_Library.Checked, int.Parse(textBox_NbSeats.Text), int.Parse(textBox_NbFaculties.Text));
                    break;
                default:
                    MessageBox.Show("We create only Residential, Commercial or Institutional estates");
                    break;
            }
            if (!change)
                 EManagerObj.Add(EstateObj);
            else
            {
                EManagerObj.ChangeAt(EstateObj, SelectedEstateList());
                change = false;
            }

            ClearForm();
        }

        /// <summary>
        /// Display all the informations of the estate and the picture
        /// </summary>
        /// 
        /*
        private void DisplayEstate()
        {
            if (EstateObj != null)
            {
                textBox_CurrentEstate.Text = EstateObj.Category + " - " + EstateObj.Type + "\n \n" + EstateObj.DisplayInfo();
                switch (EstateObj.Type)
                {
                    case Type.villa:
                        pictureBox.Image = RealEstateApp.Properties.Resources.villa;
                        break;
                    case Type.apartment:
                        pictureBox.Image = RealEstateApp.Properties.Resources.apartment;
                        break;
                    case Type.townhouse:
                        pictureBox.Image = RealEstateApp.Properties.Resources.townhouse;
                        break;
                    case Type.shop:
                        pictureBox.Image = RealEstateApp.Properties.Resources.shop;
                        break;
                    case Type.warehouse:
                        pictureBox.Image = RealEstateApp.Properties.Resources.warehouse;
                        break;
                    case Type.school:
                        pictureBox.Image = RealEstateApp.Properties.Resources.school;
                        break;
                    case Type.university:
                        pictureBox.Image = RealEstateApp.Properties.Resources.university;
                        break;
                    default:
                        break;
                }
            }
            else
                textBox_CurrentEstate.Text = "No current estate";
        }*/

        private void DisplayListEstate()
        {
            listBox1.Items.Clear();
            if (EManagerObj.M_list != null)
            {
                foreach ( var it in EManagerObj.ToStringList())
                {
                    listBox1.Items.Add(it.ToString());
                }
            }
        }
        private void DisplayEstateIndex(int anIndex)
        {
            Estate TempObj = EManagerObj.GetAt(anIndex);
            if (TempObj != null)
            {
                textBox_CurrentEstate.Text = TempObj.Category + " - " + TempObj.Type + "\n \n" + TempObj.DisplayInfo();
                switch (TempObj.Type)
                {
                    case RealEstateBLL.Type.villa:
                        pictureBox.Image = RealEstateApp.Properties.Resources.villa;
                        break;
                    case RealEstateBLL.Type.apartment:
                        pictureBox.Image = RealEstateApp.Properties.Resources.apartment;
                        break;
                    case RealEstateBLL.Type.townhouse:
                        pictureBox.Image = RealEstateApp.Properties.Resources.townhouse;
                        break;
                    case RealEstateBLL.Type.shop:
                        pictureBox.Image = RealEstateApp.Properties.Resources.shop;
                        break;
                    case RealEstateBLL.Type.warehouse:
                        pictureBox.Image = RealEstateApp.Properties.Resources.warehouse;
                        break;
                    case RealEstateBLL.Type.school:
                        pictureBox.Image = RealEstateApp.Properties.Resources.school;
                        break;
                    case RealEstateBLL.Type.university:
                        pictureBox.Image = RealEstateApp.Properties.Resources.university;
                        break;
                    default:
                        break;
                }
            }
            else
                textBox_CurrentEstate.Text = "No current estate";
        }
        //============================//
        //  LISTBOX  

        private void listBox_category_SelectedIndexChanged(object sender, EventArgs e)
        {
            int SelectedCategory = listBox_category.SelectedIndex;
            HideAllEstateFields();

            switch (SelectedCategory)
            {
                case 0:     // Residential
                    // Display the different residential estates and hide the others
                    listBox_residential.Visible = true;
                    listBox_commercial.Visible = false;
                    listBox_institutional.Visible = false;
                    // Display the residential fields and hide the others
                    ResidentialFields.Visible = true;
                    CommercialFields.Visible = false;
                    InstitutionalFields.Visible = false;
                    // Unselect commercial and institutional Types
                    listBox_commercial.ClearSelected();
                    listBox_institutional.ClearSelected();
                    break;
                case 1:     // Commercial
                    // Display the different commercial estates and hide the others
                    listBox_residential.Visible = false;
                    listBox_commercial.Visible = true;
                    listBox_institutional.Visible = false;
                    // Display the commercial fields and hide the others
                    ResidentialFields.Visible = false;
                    CommercialFields.Visible = true;
                    InstitutionalFields.Visible = false;
                    // Unselect residential and institutional Types
                    listBox_residential.ClearSelected();
                    listBox_institutional.ClearSelected();
                    break;
                case 2:     // Institutional
                    // Display the different institutional estates and hide the others
                    listBox_residential.Visible = false;
                    listBox_commercial.Visible = false;
                    listBox_institutional.Visible = true;
                    // Display the institutional fields and hide the others
                    ResidentialFields.Visible = false;
                    CommercialFields.Visible = false;
                    InstitutionalFields.Visible = true;
                    // Unselect residential and commercial Types
                    listBox_residential.ClearSelected();
                    listBox_commercial.ClearSelected();
                    break;
                default:    // None
                    // Unselect all Types
                    listBox_residential.ClearSelected();
                    listBox_commercial.ClearSelected();
                    listBox_institutional.ClearSelected();
                    break;
            }
        }

        private void listBox_residential_SelectedIndexChanged(object sender, EventArgs e)
        {
            int SelectedType = listBox_residential.SelectedIndex;
            HideAllEstateFields();

            switch (SelectedType)
            {
                case 0:     // Villa
                    groupBox_Villa.Visible = true;
                    break;
                case 1:     // Apartment
                    groupBox_Apartment.Visible = true;
                    break;
                case 2:     // Townhouse
                    groupBox_Townhouse.Visible = true;
                    break;
                default:    // None
                    break;
            }
        }

        private void listBox_commercial_SelectedIndexChanged(object sender, EventArgs e)
        {
            int SelectedType = listBox_commercial.SelectedIndex;
            HideAllEstateFields();

            switch (SelectedType)
            {
                case 0:     // Shop
                    groupBox_Shop.Visible = true;
                    break;
                case 1:     // Warehouse
                    groupBox_Warehouse.Visible = true;
                    break;
                default:    // None
                    break;
            }
        }

        private void listBox_institutional_SelectedIndexChanged(object sender, EventArgs e)
        {
            int SelectedType = listBox_institutional.SelectedIndex;
            HideAllEstateFields();

            switch (SelectedType)
            {
                case 0:     // School
                    groupBox_School.Visible = true;
                    break;
                case 1:     // University
                    groupBox_University.Visible = true;
                    break;
                default:    // None
                    break;
            }
        }

        private void listBox_PaymentSystem_SelectedIndexChanged(object sender, EventArgs e)
        {
            int SelectedPayment = listBox_PaymentSystem.SelectedIndex;
            switch (SelectedPayment)
            {
                case 0:
                    groupBox_Bank.Visible = true;
                    groupBox_WesternUnion.Visible = false;
                    groupBox_PayPal.Visible = false;
                    break;
                case 1:
                    groupBox_Bank.Visible = false;
                    groupBox_WesternUnion.Visible = true;
                    groupBox_PayPal.Visible = false;
                    break;
                case 2:
                    groupBox_Bank.Visible = false;
                    groupBox_WesternUnion.Visible = false;
                    groupBox_PayPal.Visible = true;
                    break;
                default:
                    groupBox_Bank.Visible = false;
                    groupBox_WesternUnion.Visible = false;
                    groupBox_PayPal.Visible = false;
                    break;
            }
        }

        //============================//         
        //  GET SELECTED IN THE LISTBOX      

        private LegalForm GetSelectedLegalForm()
        {
            int SelectedLegalForm = listBox_LegalForm.SelectedIndex;
            switch(SelectedLegalForm)
            {
                case 0:
                    return LegalForm.ownership;
                case 1:
                    return LegalForm.tenement;
                case 2:
                    return LegalForm.rental;
                default:
                    return LegalForm.nothing;
            }
        }

        private RealEstateBLL.Type GetSelectedType()
        {
            RealEstateBLL.Type SelectedType;
            int SelectedCategory = listBox_category.SelectedIndex;

            switch(SelectedCategory)
            {
                case 0:
                    SelectedType = GetSelectedResidential();
                    break;
                case 1:
                    SelectedType = GetSelectedCommercial();
                    break;
                case 2:
                    SelectedType = GetSelectedInstitutional();
                    break;
                default:
                    SelectedType = RealEstateBLL.Type.nothing;
                    break;
            }

            return SelectedType;
        }

        private RealEstateBLL.Type GetSelectedResidential()
        {
            int SelectedType = listBox_residential.SelectedIndex;

            switch(SelectedType)
            {
                case 0:
                    return RealEstateBLL.Type.villa;
                case 1:
                    return RealEstateBLL.Type.apartment;
                case 2:
                    return RealEstateBLL.Type.townhouse;
                default:
                    return RealEstateBLL.Type.nothing;
            }
        }

        private RealEstateBLL.Type GetSelectedCommercial()
        {
            int SelectedType = listBox_commercial.SelectedIndex;

            switch(SelectedType)
            {
                case 0:
                    return RealEstateBLL.Type.shop;
                case 1:
                    return RealEstateBLL.Type.warehouse;
                default:
                    return RealEstateBLL.Type.nothing;
            }
        }

        private RealEstateBLL.Type GetSelectedInstitutional()
        {
            int SelectedType = listBox_institutional.SelectedIndex;

            switch(SelectedType)
            {
                case 0:
                    return RealEstateBLL.Type.school;
                case 1:
                    return RealEstateBLL.Type.university;
                default:
                    return RealEstateBLL.Type.nothing;
            }
        }

        //============================//         
        //  BUTTON        

        private void buttonAddEstate_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
                AddEstate();
            else
                MessageBox.Show("Fill all the boxes to add this estate");
        }

        /// <summary>
        /// Display all the fields of the estate in the textBoxes, ... to allow the user to modify them
        /// </summary>
        private void button_Modify_Click(object sender, EventArgs e)
        {
            int anIndex;
            change = true;
            if (ValidSelectedList())
            {
                anIndex = SelectedEstateList();
                DisplayEstateIndex(anIndex);
                ClearForm();
                listBox1.SetSelected(anIndex, true);
                EstateObj = EManagerObj.GetAt(anIndex);
                textBox_ID.Text = EstateObj.Id.ToString();
                textBox_Street.Text = EstateObj.AddressEstate.Street;
                textBox_ZipCode.Text = EstateObj.AddressEstate.ZipCode.ToString();
                textBox_City.Text = EstateObj.AddressEstate.City;
                listBox_Countries.SetSelected(EstateObj.AddressEstate.Country, true);
                listBox_LegalForm.SetSelected((int)EstateObj.LegalForm - 1, true);

                if (EstateObj is Residential)
                {
                    listBox_category.SetSelected(0, true);
                    textBox_NbRooms.Text = ((Residential)EstateObj).NbOfRooms.ToString();

                    if (EstateObj is Villa)
                    {
                        listBox_residential.SetSelected(0, true);
                        checkBox_Pool.Checked = ((Villa)EstateObj).Pool;
                    }
                    else if (EstateObj is Apartment)
                    {
                        listBox_residential.SetSelected(1, true);
                        checkBox_Balcony.Checked = ((Apartment)EstateObj).Balcony;
                    }
                    else if (EstateObj is Townhouse)
                    {
                        listBox_residential.SetSelected(2, true);
                        textBox_GardenSize.Text = ((Townhouse)EstateObj).GardenSize.ToString();
                    }
                }
                else if (EstateObj is Commercial)
                {
                    listBox_category.SetSelected(1, true);
                    textBox_NbFloors.Text = ((Commercial)EstateObj).NbFloors.ToString();

                    if (EstateObj is Shop)
                    {
                        listBox_commercial.SetSelected(0, true);
                        textBox_VitrineLength.Text = ((Shop)EstateObj).VitrineLength.ToString();
                    }
                    else if (EstateObj is Warehouse)
                    {
                        listBox_commercial.SetSelected(1, true);
                        textBox_NbShelfs.Text = ((Warehouse)EstateObj).NbShelfs.ToString();
                    }
                }
                else if (EstateObj is Institutional)
                {
                    listBox_category.SetSelected(2, true);
                    textBox_NbSeats.Text = ((Institutional)EstateObj).NbSeatsCafeteria.ToString();
                    checkBox_Library.Checked = ((Institutional)EstateObj).Library;

                    if (EstateObj is School)
                    {
                        listBox_institutional.SetSelected(0, true);
                        textBox_NbClassrooms.Text = ((School)EstateObj).NbClassrooms.ToString();
                    }
                    else if (EstateObj is University)
                    {
                        listBox_institutional.SetSelected(1, true);
                        textBox_NbFaculties.Text = ((University)EstateObj).NbFaculties.ToString();
                    }
                }

                textBox_Amount.Text = EstateObj.PaymentSystem.Amount.ToString();
                textBox_Options.Text = EstateObj.PaymentSystem.Options;
                if(EstateObj.PaymentSystem is Bank bank)
                {
                    listBox_PaymentSystem.SetSelected(0, true);
                    textBox_Name.Text = bank.Name;
                    textBox_AccountNumber.Text = bank.AccountNumber.ToString();
                }
                else if(EstateObj.PaymentSystem is WesternUnion wu)
                {
                    listBox_PaymentSystem.SetSelected(1, true);
                    textBox_NameWU.Text = wu.Name;
                    textBox_Email.Text = wu.Email;
                }
                else if(EstateObj.PaymentSystem is PayPal pp)
                {
                    listBox_PaymentSystem.SetSelected(2, true);
                    textBox_EmailPayPal.Text = pp.Email;
                }

                MessageBox.Show("Modify the fields you need, then click again on Add this estate");
            }
            else
                MessageBox.Show("There is no current estate Selected");

        }

        private void button_Delete_Click(object sender, EventArgs e)
        {
            EstateObj = null;
            if (ValidSelectedList())
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure?", "Delete", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    EManagerObj.DeleteAt(listBox1.SelectedIndex);
                }
                else if (dialogResult == DialogResult.No)
                {
                    //do something else
                }
            }
            else
            {
                MessageBox.Show("There is no current estate Selected");
            }
            ClearForm();
        }

        private void groupBox_DisplayVilla_Enter(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int anIndex = listBox1.SelectedIndex;
            if (anIndex != -1)
            DisplayEstateIndex(anIndex);
        }

        private int SelectedEstateList()
        {
            return listBox1.SelectedIndex;
        }

        private bool ValidSelectedList()
        {
            if (listBox1.SelectedIndex == -1)
                return false;
            else
                return true;
        }

        private void buttonDeleteAll_Click(object sender, EventArgs e)
        {
            EstateObj = null;
                DialogResult dialogResult = MessageBox.Show("Are you sure?", "Delete All", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    EManagerObj.DeleteAll();
                }
                else if (dialogResult == DialogResult.No)
                {
                    //do something else
                }
            
            ClearForm();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure?", "Exit", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                System.Windows.Forms.Application.Exit();
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
            
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure?", "New", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                //save 
                EManagerObj.DeleteAll();
                ClearForm();
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }

        public static string BinaryFileSerialize<T> (string filePath, T obj)
        {
            FileStream fileStream = null;
            string errorMsg = null;
            try
            {
                fileStream = new FileStream(filePath, FileMode.Create);
                BinaryFormatter b = new BinaryFormatter();
                b.Serialize(fileStream, obj);
                
            }
            catch(Exception e)
            {
                errorMsg = e.Message;
            }
            finally
            {
                if (fileStream != null)
                    fileStream.Close();
            }
            return errorMsg;
        }

        public static T BinaryFileDeSerialize<T>(string filePath, out string errorMessage)
        {
            FileStream fileStream = null;
            errorMessage = null;

            Object obj = null;
            try
            {
                if(!File.Exists(filePath))
                {
                    errorMessage = $"The file {filePath} was not found. ";
                    throw (new FileNotFoundException(errorMessage));
                }
                using (StringReader reader = new StringReader(filePath))//
                {
                    fileStream = new FileStream(filePath, FileMode.Open);
                    BinaryFormatter b = new BinaryFormatter();
                    obj = (List<Estate>)b.Deserialize(fileStream);

                }
                  

            }
            catch(Exception e)
            {
                if (errorMessage != null)
                    errorMessage = e.Message;
            }
            return (T)obj;
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure?", "Save As", MessageBoxButtons.YesNo);
            
            if (dialogResult == DialogResult.Yes)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    fileName = saveFileDialog1.FileName;
                    BinaryFileSerialize(fileName, EManagerObj.M_list);
                }

            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            
       
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure?", "Save", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (fileName == String.Empty)
                {
                    saveAsToolStripMenuItem_Click(sender, e);
                }

                else
                    BinaryFileSerialize(fileName, EManagerObj.M_list);
            }
            
        }

        private void AskIfSaveData()
        {
            DialogResult dialogResult = MessageBox.Show("Do You Want To Save Before", "Save", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                //saveToolStripMenuItem_Click(sender, e);
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AskIfSaveData();
            string errorM;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileName = openFileDialog1.FileName;
                EstateObj = null;
                EManagerObj.DeleteAll();
                EManagerObj.M_list =  BinaryFileDeSerialize<List<Estate>>(fileName, out errorM);
                if (errorM != null)
                    MessageBox.Show(errorM);
                ClearForm();
            }
        }

        /*public static T FromXML<T>(string xml)
        {
            using (StringReader stringReader = new StringReader(xml))
            {
                XmlSerializer serializer = new XmlSerializer(Typeof(T));
                return (T)serializer.Deserialize(stringReader);
            }
        }*/

        public static void ToXML<T>(T obj, string filePath)
        {
            /*
            string errorM = null;
            try
            {
                using (StringWriter stringWriter = new StringWriter(filePath))//
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(Typeof(T));
                    xmlSerializer.Serialize(stringWriter, obj);
                    stringWriter.Close();
                }
                
            }
            catch(Exception e)
            {
                //errorM = e.Message;
            }*/

        }

        private void importFromXMLFileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exportToXMLFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure?", "Export To XML", MessageBoxButtons.YesNo);
            //string XMLText;
            if (dialogResult == DialogResult.Yes)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    fileName = saveFileDialog1.FileName;
                    ToXML(EManagerObj.M_list, fileName);
                }
            }
        }
    }
}
