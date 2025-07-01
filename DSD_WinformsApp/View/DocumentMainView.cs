using DSD_WinformsApp.Core.DTOs;
using DSD_WinformsApp.Infrastructure.Data;
using DSD_WinformsApp.Infrastructure.Data.Services;
using DSD_WinformsApp.Model;
using DSD_WinformsApp.Presenter;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WordApp = Microsoft.Office.Interop.Word.Application;
using ExcelApp = Microsoft.Office.Interop.Excel.Application;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace DSD_WinformsApp.View
{
    public partial class DocumentMainView : Form, IDocumentView
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDocumentPresenter _presenter;

        private bool isNewFileUploaded = false;

        private bool isVisible;

        private bool isUploadSuccessful = false;

        private ErrorProvider errorProvider = null!;

        public DocumentMainView(IUnitOfWork unitOfWork)
        {
            InitializeComponent();
            _unitOfWork = unitOfWork;
            _presenter = new DocumentPresenter(this, _unitOfWork.Documents, _unitOfWork.BackUpFiles, _unitOfWork.Users);

            WindowState = FormWindowState.Maximized; // Set the initial window state to Maximized

            this.FormClosing += DocumentViewForm_FormClosing; // Form closing event handler

            errorProvider = new ErrorProvider();

            ToggleAdminRights(isVisible); // Manage Users button visibility

            // Documents filter events 
            textBoxSearchBar.TextChanged += textBoxSearchBar_TextChanged;
            comboBoxCategoryDropdown.SelectedIndexChanged += comboBoxCategoryDropdown_SelectedIndexChanged;

            // Users filter events
            textBoxUsersSearchBox.TextChanged += textBoxUsersSearchBox_TextChanged;
            comboBox_JobCategory.SelectedIndexChanged += comboBox_JobCategory_SelectedIndexChanged;

            #region Manage Users Events
            // Attach TextChanged event handlers to the relevant text fields
            textBoxUserFirstName.TextChanged += TextBoxUsers_TextChanged;
            textBoxUserLastName.TextChanged += TextBoxUsers_TextChanged;
            textBoxUserEmailAdd.TextChanged += TextBoxUsers_TextChanged;
            textBoxUserJobTitle.TextChanged += TextBoxUsers_TextChanged;

            // Attach CheckedChanged event handler to the checkbox
            checkBoxEnableAdmin.CheckedChanged += CheckBox_CheckedChanged;

            // Store the original state of the checkbox
            originalCheckBoxState = checkBoxEnableAdmin.Checked;

            #endregion
        }

        private async void DocumentView_Load_1(object sender, EventArgs e)
        {
            errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;

            string varDocSearchQuery = GetSearchQuery();
            string varDocCategoryQuery = GetFilterCategory();
            string varUserSearchQuery = GetSearchUserQuery();
            string varUserJobcatergoryQuery = GetFilterUsersCategory();

            await _presenter.LoadDocumentsByFilter(varDocSearchQuery, varDocCategoryQuery); // Load the documents by filter using the presenter

            await _presenter.LoadUsersByFilter(varUserSearchQuery, varUserJobcatergoryQuery); // Load the users from the database using the presenter

            panelHome.Visible = true;
            panelDocumentButton.Visible = false;
            panelManageUsers.Visible = false;
            panelUserDetails.Visible = false;

            #region Document Page Properties

            // Add controls into panelDocumentButton
            panelDocumentButton.Controls.Add(pictureBox1);
            panelDocumentButton.Controls.Add(dataGridView1);

            textBoxSearchBar.Height = 100;
            textBoxSearchBar.Padding = new Padding(5);

            // Create instance for comboBoxCategoryDropdown items
            comboBoxCategoryDropdown.Items.Add("All Categories");
            comboBoxCategoryDropdown.Items.Add("Board Resolutions");
            comboBoxCategoryDropdown.Items.Add("Canteen Policies");
            comboBoxCategoryDropdown.Items.Add("COOP Policies");
            comboBoxCategoryDropdown.Items.Add("COOP Article & By Laws");
            comboBoxCategoryDropdown.Items.Add("Minutes of the Meeting");
            comboBoxCategoryDropdown.Items.Add("Regulatory Requirements");

            // Add details button functionality
            DataGridViewButtonColumn detailsColumn = new DataGridViewButtonColumn();
            detailsColumn.Text = "Details";
            detailsColumn.Name = "Details";
            detailsColumn.Width = 93;
            detailsColumn.UseColumnTextForButtonValue = true;
            detailsColumn.HeaderText = string.Empty;
            detailsColumn.FlatStyle = FlatStyle.Flat;
            detailsColumn.DefaultCellStyle.ForeColor = Color.Blue;
            detailsColumn.DefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Underline);
            dataGridView1.Columns.Add(detailsColumn);

            // Wire up the CellClick event handler
            dataGridView1.CellClick += dataGridView1_DetailsButton_CellClick;

            // Set the cursor to hand when hovering over the Details button
            dataGridView1.CellMouseEnter += (sender, e) =>
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
                {
                    dataGridView1.Cursor = Cursors.Hand;
                }
                else
                {
                    dataGridView1.Cursor = Cursors.Default; // Set the default cursor for other cells
                }
            };

            #endregion

            #region Manage Users Properties

            // Create instance for comboBox_JobCategory items
            comboBox_JobCategory.Items.Add("All Job Titles");
            comboBox_JobCategory.Items.Add("Manager");
            comboBox_JobCategory.Items.Add("Staff");

            // Datagridviewbutton details column
            DataGridViewButtonColumn detailsButtonUserColumn = new DataGridViewButtonColumn();
            detailsButtonUserColumn.Name = "Details";
            detailsButtonUserColumn.Text = "Details";
            detailsButtonUserColumn.Width = 91;
            detailsButtonUserColumn.HeaderText = string.Empty;
            detailsButtonUserColumn.UseColumnTextForButtonValue = true;
            detailsButtonUserColumn.FlatStyle = FlatStyle.Flat;
            detailsButtonUserColumn.DefaultCellStyle.ForeColor = Color.Blue;
            detailsButtonUserColumn.DefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Underline);
            dataGridViewManageUsers.Columns.Add(detailsButtonUserColumn);

            // Datagridviewbutton delete column
            DataGridViewButtonColumn deleteButtonUserColumn = new DataGridViewButtonColumn();
            deleteButtonUserColumn.Name = "Delete";
            deleteButtonUserColumn.Text = "Delete";
            deleteButtonUserColumn.Width = 91;
            deleteButtonUserColumn.HeaderText = string.Empty;
            deleteButtonUserColumn.UseColumnTextForButtonValue = true;
            deleteButtonUserColumn.FlatStyle = FlatStyle.Flat;
            deleteButtonUserColumn.DefaultCellStyle.ForeColor = Color.Red;
            deleteButtonUserColumn.DefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Underline);
            dataGridViewManageUsers.Columns.Add(deleteButtonUserColumn);

            // Wire up the CellClick event handler
            dataGridViewManageUsers.CellClick += dataGridViewManageUsers_DetailsButton_CellClick;
            dataGridViewManageUsers.CellClick += dataGridViewManageUsers_DeleteButton_CellClick;

            // Set the cursor to hand if button columns only
            dataGridViewManageUsers.CellMouseEnter += (sender, e) =>
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dataGridViewManageUsers.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
                {
                    dataGridViewManageUsers.Cursor = Cursors.Hand;
                }
                else
                {
                    dataGridViewManageUsers.Cursor = Cursors.Default; // Set the default cursor for other cells
                }
            };

            #endregion
        }

        public void BindDataMainView(List<DocumentDto> documents) => dataGridView1.DataSource = documents; // Bind documents to dataGridView1

        public void BindDataManageUsers(List<UserCredentialsDto> users) => dataGridViewManageUsers.DataSource = users; // Bind users to dataGridViewManageUsers


        #region Panel Buttons Events

        private void buttonHome_Click(object sender, EventArgs e)
        {
            // Set panelHome as visible only
            panelHome.Visible = true;
            panelDocumentButton.Visible = false;
            panelManageUsers.Visible = false;
            panelUserDetails.Visible = false;
        }

        private void buttonDocument_Click(object sender, EventArgs e)
        {
            // Set panelDocument as visible only
            panelDocumentButton.Visible = true;
            panelHome.Visible = false;
            panelManageUsers.Visible = false;
            panelUserDetails.Visible = false;

            // Define the column width from documentmodel
            dataGridView1.Columns["DocumentVersion"].Width = 200;
            dataGridView1.Columns["Filename"].Width = 600;
            dataGridView1.Columns["Category"].Width = 360;
            dataGridView1.Columns["Status"].Width = 185;
            dataGridView1.Columns["CreatedDate"].Width = 186;
            dataGridView1.Columns["Id"].Visible = false;
            dataGridView1.Columns["CreatedBy"].Visible = false;
            dataGridView1.Columns["ModifiedBy"].Visible = false;
            dataGridView1.Columns["ModifiedDate"].Visible = false;
            dataGridView1.Columns["Notes"].Visible = false;
            dataGridView1.Columns["FileData"].Visible = false;
            dataGridView1.Columns["FilenameExtension"].Visible = false;

            // Display name for table columns
            dataGridView1.Columns["DocumentVersion"].HeaderText = "DOCUMENT NO.";
            dataGridView1.Columns["Status"].HeaderText = "STATUS";
            dataGridView1.Columns["Filename"].HeaderText = "DOCUMENT TITLE";
            dataGridView1.Columns["Category"].HeaderText = "CATEGORY";
            dataGridView1.Columns["CreatedDate"].HeaderText = "CREATED DATE";

            // Set the font for the header cells
            dataGridView1.Columns["DocumentVersion"].HeaderCell.Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dataGridView1.Columns["Status"].HeaderCell.Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dataGridView1.Columns["Filename"].HeaderCell.Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dataGridView1.Columns["Category"].HeaderCell.Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dataGridView1.Columns["CreatedDate"].HeaderCell.Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);


            // make header text center
            dataGridView1.Columns["DocumentVersion"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["Status"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["Filename"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["Category"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["CreatedDate"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // force header cell to have color
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#576CBC");
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            // Enable header cell height
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridView1.ColumnHeadersHeight = 50;

            // Disable cell highlight when click
            dataGridView1.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#FFFFFF");
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;
        }

        private async void buttonManageUsers_Click(object sender, EventArgs e)
        {
            // Set panelManageUsers as visible only
            panelManageUsers.Visible = true;
            panelHome.Visible = false;
            panelDocumentButton.Visible = false;
            panelUserDetails.Visible = false;

            var currentUsersSearchQueryWhenItemDeleted = GetSearchUserQuery();
            var currentUsersFilterCategoryWhenItemDeleted = GetFilterUsersCategory();

            await _presenter.LoadUsersByFilter(currentUsersSearchQueryWhenItemDeleted, currentUsersFilterCategoryWhenItemDeleted);

            panelManageUsers.Controls.Add(dataGridViewManageUsers);

            // Set the datagridviewManageUsers column properties
            dataGridViewManageUsers.Columns["UserId"].DisplayIndex = 0;
            dataGridViewManageUsers.Columns["Firstname"].DisplayIndex = 1;
            dataGridViewManageUsers.Columns["Lastname"].DisplayIndex = 2;
            dataGridViewManageUsers.Columns["EmailAddress"].DisplayIndex = 3;
            dataGridViewManageUsers.Columns["JobTitle"].DisplayIndex = 4;
            dataGridViewManageUsers.Columns["UserRole"].DisplayIndex = 5;

            // Set the column widths
            dataGridViewManageUsers.Columns["Firstname"].Width = 245;
            dataGridViewManageUsers.Columns["Lastname"].Width = 245;
            dataGridViewManageUsers.Columns["EmailAddress"].Width = 470;
            dataGridViewManageUsers.Columns["JobTitle"].Width = 280;
            dataGridViewManageUsers.Columns["UserRole"].Width = 200;

            dataGridViewManageUsers.Columns["Firstname"].Visible = true;
            dataGridViewManageUsers.Columns["Lastname"].Visible = true;
            dataGridViewManageUsers.Columns["EmailAddress"].Visible = true;
            dataGridViewManageUsers.Columns["JobTitle"].Visible = true;
            dataGridViewManageUsers.Columns["UserRole"].Visible = true;
            dataGridViewManageUsers.Columns["UserId"].Visible = false;
            dataGridViewManageUsers.Columns["CreatedDate"].Visible = false;
            dataGridViewManageUsers.Columns["Password"].Visible = false;
            dataGridViewManageUsers.Columns["ImageData"].Visible = false;
            dataGridViewManageUsers.Columns["Username"].Visible = false;

            // Display name for headertext
            dataGridViewManageUsers.Columns["Firstname"].HeaderText = "FIRST NAME";
            dataGridViewManageUsers.Columns["Lastname"].HeaderText = "LAST NAME";
            dataGridViewManageUsers.Columns["EmailAddress"].HeaderText = "EMAIL ADDRESS";
            dataGridViewManageUsers.Columns["JobTitle"].HeaderText = "JOB TITLE";
            dataGridViewManageUsers.Columns["UserRole"].HeaderText = "USER ROLE";

            // Set header text to center
            dataGridViewManageUsers.Columns["Firstname"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewManageUsers.Columns["Lastname"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewManageUsers.Columns["EmailAddress"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewManageUsers.Columns["JobTitle"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewManageUsers.Columns["UserRole"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Set header text to bold
            dataGridViewManageUsers.Columns["Firstname"].HeaderCell.Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dataGridViewManageUsers.Columns["Lastname"].HeaderCell.Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dataGridViewManageUsers.Columns["EmailAddress"].HeaderCell.Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dataGridViewManageUsers.Columns["JobTitle"].HeaderCell.Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dataGridViewManageUsers.Columns["UserRole"].HeaderCell.Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);

            // Force header cell to have color
            dataGridViewManageUsers.EnableHeadersVisualStyles = false;
            dataGridViewManageUsers.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#576CBC");
            dataGridViewManageUsers.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            // Enable cell header height
            dataGridViewManageUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewManageUsers.ColumnHeadersHeight = 50;

            // Force disable cell highlight color
            dataGridViewManageUsers.DefaultCellStyle.SelectionBackColor = Color.White;
            dataGridViewManageUsers.DefaultCellStyle.SelectionForeColor = Color.Black;
        }

        #endregion

        #region Document Page Methods

        private void dataGridView1_DetailsButton_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView1.Columns["Details"].Index)
            {
                DocumentDto selectedDocument = (DocumentDto)dataGridView1.Rows[e.RowIndex].DataBoundItem;
                ShowDocumentDetailsModal(selectedDocument);
            }
        }

        private async void ShowDocumentDetailsModal(DocumentDto selectedDocument)
        {
            // check user access base on labelHomePageUserLogin in document page.
            bool isAdmin = await _presenter.CheckUserAccess(labelHomePageUserLogin.Text);

            // Create a new form to display the document details (modal form).
            DetailsFormView detailsForm = new DetailsFormView();
            detailsForm.Text = "Document Details Form";
            detailsForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            detailsForm.StartPosition = FormStartPosition.CenterParent;
            detailsForm.MaximizeBox = false;
            detailsForm.MinimizeBox = false;
            detailsForm.Cursor = Cursors.Default;

            // Create the buttons and add them to the detailsForm
            CustomButton button1 = new CustomButton(ColorTranslator.FromHtml("#A5D7E8"), SystemColors.Control);
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.MouseOverBackColor = Color.FromArgb(9, 142, 154);
            button1.FlatStyle = FlatStyle.Flat;
            button1.Text = "DETAILS";
            button1.Location = new Point(20, 35);
            button1.Height = 40;
            button1.Width = 130;
            button1.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            button1.Visible = isAdmin;
            button1.ForeColor = Color.White;
            detailsForm.Controls.Add(button1);

            CustomButton button2 = new CustomButton(ColorTranslator.FromHtml("#A5D7E8"), SystemColors.Control);
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatAppearance.MouseOverBackColor = Color.FromArgb(9, 142, 154);
            button2.FlatStyle = FlatStyle.Flat;
            button2.Text = "HISTORY";
            button2.Location = new Point(button1.Right + 20, 35);
            button2.Height = button1.Height;
            button2.Width = button1.Width;
            button2.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            button2.ForeColor = Color.White;
            button2.Visible = isAdmin; // Hide the button if the user is not an admin
            detailsForm.Controls.Add(button2);

            #region Document Details
            // Create the GroupBox
            GroupBox groupBox = new GroupBox();
            groupBox.Text = "Document Details";
            groupBox.AutoSize = false;
            groupBox.Width = detailsForm.ClientSize.Width;
            groupBox.Height = detailsForm.ClientSize.Height;
            groupBox.Location = new Point(20, 80); ;
            groupBox.Visible = true;

            // Add the GroupBox to the detailsForm
            detailsForm.Controls.Add(groupBox);

            int textBoxWidth = 450; // You can adjust the default width for TextBox controls

            // Create download button
            CustomButton downloadButton = new CustomButton(ColorTranslator.FromHtml("#A5D7E8"), SystemColors.Control);
            downloadButton.FlatAppearance.BorderSize = 0;
            downloadButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(9, 142, 154);
            downloadButton.FlatStyle = FlatStyle.Flat;
            downloadButton.Text = "Download";
            downloadButton.Name = "downloadButton";
            downloadButton.Location = new Point(groupBox.Right - (downloadButton.Width + 120), groupBox.Top - 50);
            downloadButton.Height = 40;
            downloadButton.Width = 110;
            downloadButton.Click += (sender, e) =>
            {

                // Show the delete confirmation modal directly in the main form.
                DialogResult result = MessageBox.Show(detailsForm, $"Do you want to download {selectedDocument.Filename}", "Download Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    downloadButton.Enabled = false;
                    ConfirmDownloadDocument(selectedDocument, isAdmin);
                    downloadButton.Enabled = true; // Enable the button again after the download is complete
                }

            };
            groupBox.Controls.Add(downloadButton);

            // Create delete button
            CustomButton deleteButton = new CustomButton(ColorTranslator.FromHtml("#DA0B0B"), SystemColors.Control);
            deleteButton.FlatAppearance.BorderSize = 0;
            deleteButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(9, 142, 154);
            deleteButton.FlatStyle = FlatStyle.Flat;
            deleteButton.Text = "Delete";
            deleteButton.Name = "deleteButton";
            deleteButton.Location = new Point(downloadButton.Left - (downloadButton.Width + 20), downloadButton.Top);
            deleteButton.Height = 40;
            deleteButton.Width = 110;
            deleteButton.Visible = isAdmin;
            deleteButton.Click += async (sender, e) =>
            {
                DialogResult result = MessageBox.Show(detailsForm, $"Do you want to delete {selectedDocument.Filename}?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question); // Show the delete confirmation modal directly in the main form.

                if (result == DialogResult.Yes)
                {
                    await _presenter.DeleteDocumentWithBackups(selectedDocument); // Delete the document and its backups from the database.

                    // Get the current search query and filter category
                    var currentSearchQueryWhenItemDeleted = GetSearchQuery();
                    var currentFilterCategoryWhenItemDeleted = GetFilterCategory();

                    await _presenter.LoadDocumentsByFilter(currentSearchQueryWhenItemDeleted, currentFilterCategoryWhenItemDeleted); // Load the filtered documents again to update the view

                    detailsForm.Close(); // Close the detailsForm
                }
            };
            groupBox.Controls.Add(deleteButton);

            // Create a TextBox for "Filename"
            TextBox filenameTextBox = new TextBox();
            filenameTextBox.Text = selectedDocument.Filename;
            filenameTextBox.ReadOnly = true;
            filenameTextBox.Multiline = true;
            filenameTextBox.Height = 36;
            int filenameTextBoxWidth = textBoxWidth - 130;
            AddRow(groupBox, "Document Title:", filenameTextBox, filenameTextBoxWidth);

            // Create the "Upload File" button and pass the filenameTextBox as a parameter
            CustomButton uploadFileButton = new CustomButton(ColorTranslator.FromHtml("#A5D7E8"), SystemColors.Control);
            uploadFileButton.FlatAppearance.BorderSize = 0;
            uploadFileButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(9, 142, 154);
            uploadFileButton.FlatStyle = FlatStyle.Flat;
            uploadFileButton.Text = "Upload File";
            uploadFileButton.Location = new Point(filenameTextBox.Right + 10, filenameTextBox.Top - 5);
            uploadFileButton.Height = filenameTextBox.Height + 6;
            uploadFileButton.Width = 120;
            uploadFileButton.Padding = new Padding(0);
            uploadFileButton.Enabled = false;
            uploadFileButton.Click += (sender, e) => UploadFileButton_Click(sender, e, filenameTextBox);
            groupBox.Controls.Add(uploadFileButton);

            // Create textbox for Document Version
            TextBox documentVersionTextBox = new TextBox();
            documentVersionTextBox.Text = selectedDocument.DocumentVersion;
            documentVersionTextBox.ReadOnly = true;
            documentVersionTextBox.Multiline = true;
            documentVersionTextBox.Height = 36;
            int documentVersionTextBoxWidth = textBoxWidth;
            AddRow(groupBox, "Document Number:", documentVersionTextBox, documentVersionTextBoxWidth);

            // Create the Category ComboBox
            ComboBox categoryComboBox = new ComboBox();
            categoryComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            categoryComboBox.Items.Add("Board Resolutions");
            categoryComboBox.Items.Add("Canteen Policies");
            categoryComboBox.Items.Add("COOP Policies");
            categoryComboBox.Items.Add("COOP Article & By Laws");
            categoryComboBox.Items.Add("Minutes of the Meeting");
            categoryComboBox.Items.Add("Regulatory Requirements");
            categoryComboBox.Text = selectedDocument.Category.ToString();
            categoryComboBox.Enabled = false;
            categoryComboBox.Height = 36;

            int categoryComboBoxWidth = textBoxWidth;
            AddRow(groupBox, "Category:", categoryComboBox, categoryComboBoxWidth);

            // Create the Status ComboBox
            ComboBox statusComboBox = new ComboBox();
            statusComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            statusComboBox.Items.Add("New");
            statusComboBox.Items.Add("Revised");
            statusComboBox.Items.Add("Obsolete");
            statusComboBox.SelectedItem = selectedDocument.Status.ToString();
            statusComboBox.Enabled = false;
            statusComboBox.Height = 36;
            int statusComboBoxWidth = textBoxWidth;
            AddRow(groupBox, "Status:", statusComboBox, statusComboBoxWidth);

            // Create a TextBox for the "Created Date" property and set its initial value
            TextBox createdDateTextBox = new TextBox();
            createdDateTextBox.Text = selectedDocument.CreatedDate.ToString("yyyy-MM-dd");
            createdDateTextBox.ReadOnly = true;
            createdDateTextBox.Multiline = true;
            createdDateTextBox.Height = 36;
            int createdDateTextBoxWidth = textBoxWidth; // Adjust the width as needed
            AddRow(groupBox, "Created Date:", createdDateTextBox, createdDateTextBoxWidth);

            // Create a TextBox for the "Created By" property and set its initial value
            TextBox createdByTextBox = new TextBox();
            createdByTextBox.Text = selectedDocument.CreatedBy;
            createdByTextBox.ReadOnly = true;
            createdByTextBox.Multiline = true;
            createdByTextBox.Height = 36;
            int createdByTextBoxWidth = textBoxWidth;
            AddRow(groupBox, "Created By:", createdByTextBox, createdByTextBoxWidth);

            // Create a TextBox for the "Modified By" property and set its initial value
            TextBox modifiedByTextBox = new TextBox();
            modifiedByTextBox.Text = string.IsNullOrEmpty(selectedDocument.ModifiedBy) ? "No data available" : selectedDocument.ModifiedBy;
            modifiedByTextBox.ReadOnly = true;
            modifiedByTextBox.Multiline = true;
            modifiedByTextBox.Height = 36;
            int modifiedByTextBoxWidth = textBoxWidth;
            AddRow(groupBox, "Modified By:", modifiedByTextBox, modifiedByTextBoxWidth);

            // Create a TextBox for the "Modified Date" property and set its initial value
            TextBox modifiedDateTextBox = new TextBox();
            modifiedDateTextBox.Text = selectedDocument.ModifiedDate?.ToString("yyyy-MM-dd") ?? "No data available";
            modifiedDateTextBox.ReadOnly = true;
            modifiedDateTextBox.Multiline = true;
            modifiedDateTextBox.Height = 36;
            int modifiedDateTextBoxWidth = textBoxWidth;
            AddRow(groupBox, "Modified Date:", modifiedDateTextBox, modifiedDateTextBoxWidth);

            // Create a multiline TextBox for the "Notes" property
            TextBox notesTextBox = new TextBox();
            notesTextBox.Text = selectedDocument.Notes;
            notesTextBox.ReadOnly = true;
            notesTextBox.Multiline = true;
            notesTextBox.MaxLength = 200;
            notesTextBox.Height = 100;

            int notesTextBoxWidth = textBoxWidth; // Adjust the width as needed
            AddRow(groupBox, "Notes:", notesTextBox, notesTextBoxWidth);

            // Function to add a row (label + control) to the GroupBox
            void AddRow(GroupBox parent, string labelText, Control control, int controlWidth)
            {
                Label label = new Label();
                label.Text = labelText;
                label.AutoSize = true;

                int labelTop = parent.Controls.Count * 25 + 50;
                label.Location = new Point(50, labelTop);

                int controlLeft = label.Right + 80;
                control.Location = new Point(controlLeft, labelTop);
                control.Width = controlWidth;

                parent.Controls.Add(label);
                parent.Controls.Add(control);
            }

            // Adjust the size of the GroupBox to fit its contents
            int groupBoxWidth = 800;
            int groupBoxHeight = groupBox.Controls.Count * 30 + 30;
            groupBox.Width = groupBoxWidth;
            groupBox.Height = groupBoxHeight;

            #endregion

            #region Document History

            // Create the GroupBox containing the second DataGridView (DataGridView2)
            GroupBox groupBox2 = new GroupBox();
            groupBox2.Text = "Document History";
            groupBox2.AutoSize = true;
            groupBox2.Location = new Point(20, button2.Bottom + 20); // Adjust the position as needed
            groupBox2.Visible = false; // Set the initial visibility to false
            detailsForm.Controls.Add(groupBox2);

            DataGridView dataGridView2 = new DataGridView();
            dataGridView2.Dock = DockStyle.Fill;
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.RowHeadersVisible = false;
            dataGridView2.ReadOnly = true;
            dataGridView2.RowTemplate.Height = 36;
            dataGridView2.BorderStyle = BorderStyle.None;
            dataGridView2.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#FFFFFF");
            dataGridView2.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridView2.ScrollBars = ScrollBars.Both;
            dataGridView2.BackgroundColor = ColorTranslator.FromHtml("#FFFFFF");
            groupBox2.Controls.Add(dataGridView2);


            // Adjust the size of the DetailsFormView to fit the contents including groupBox2
            int groupBox2Width = detailsForm.ClientSize.Width - 50;
            int groupBox2Height = detailsForm.ClientSize.Height - groupBox2.Top - 40;
            groupBox2.Size = new Size(groupBox2Width, groupBox2Height);

            // Binding related backup file data to dataGridView2 based on the selected document's ID
            var relatedBackupFiles = await _presenter.GetRelatedBackupFiles(selectedDocument.Id);
            dataGridView2.DataSource = relatedBackupFiles;


            // Display Columns in datagridview2
            dataGridView2.Columns["DocumentVersion"].Width = 180;
            dataGridView2.Columns["Filename"].Width = 400;
            dataGridView2.Columns["BackupDate"].Width = 120;
            dataGridView2.Columns["Version"].Width = 120;

            dataGridView2.Columns["DocumentVersion"].HeaderText = "Document No.";
            dataGridView2.Columns["Filename"].HeaderText = "Document Title";
            dataGridView2.Columns["BackupDate"].HeaderText = "Date";
            dataGridView2.Columns["Version"].HeaderText = "Version No.";

            dataGridView2.Columns["BackupId"].Visible = false;
            dataGridView2.Columns["OriginalFilePath"].Visible = false;
            dataGridView2.Columns["BackupFilePath"].Visible = false;
            dataGridView2.Columns["Id"].Visible = false;

            // set header cell height 
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridView2.ColumnHeadersHeight = 50;

            // Set header cell text in middle
            dataGridView2.Columns["DocumentVersion"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView2.Columns["Filename"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView2.Columns["BackupDate"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView2.Columns["Version"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // set header text to segoe bold
            dataGridView2.Columns["DocumentVersion"].HeaderCell.Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dataGridView2.Columns["Filename"].HeaderCell.Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dataGridView2.Columns["BackupDate"].HeaderCell.Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dataGridView2.Columns["Version"].HeaderCell.Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);

            // Set background color for header same with datagridview1
            dataGridView2.EnableHeadersVisualStyles = false;
            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#576CBC");
            dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            // Set the cursor to hand when hovering over the Details button
            dataGridView2.CellMouseEnter += (sender, e) =>
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dataGridView2.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
                {
                    dataGridView2.Cursor = Cursors.Hand;
                }
                else
                {
                    dataGridView2.Cursor = Cursor.Current; // Set the default cursor for other cells
                }
            };

            // Add download button functionality
            DataGridViewButtonColumn downloadColumn = new DataGridViewButtonColumn();
            downloadColumn.Text = "Download";
            downloadColumn.Name = "";
            downloadColumn.Width = 100;
            downloadColumn.UseColumnTextForButtonValue = true;
            downloadColumn.FlatStyle = FlatStyle.Flat;
            downloadColumn.DefaultCellStyle.ForeColor = Color.Green;
            downloadColumn.DefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Underline);
            dataGridView2.Columns.Add(downloadColumn);

            // Subscribe to the CellClick event of the DataGridView for download button.
            dataGridView2.CellClick += (sender, e) =>
            {
                // Check if the clicked cell is in the "Download" button column
                if (e.ColumnIndex == downloadColumn.Index && e.RowIndex >= 0)
                {
                    // Get the BackUpFileDto associated with the clicked row
                    if (dataGridView2.Rows[e.RowIndex].DataBoundItem is BackUpFileDto selectedBackupFile)
                    {
                        try
                        {
                            string sourceFilePath = selectedBackupFile.BackupFilePath; // Backup file path
                            string downloadsPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile); // Downloads folder path
                            string destinationFilePath = Path.Combine(downloadsPath, "Downloads", selectedBackupFile.Filename);

                            File.Copy(sourceFilePath, destinationFilePath, true); // Copy the file to the Downloads folder

                            // Show a message to indicate the download completion
                            MessageBox.Show(detailsForm, $"{selectedBackupFile.Filename} downloaded successfully!", "Download Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show(detailsForm, $"An error occured while downloading the document.", "Download Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            };

            // Add delete button functionality
            DataGridViewButtonColumn deleteColumn = new DataGridViewButtonColumn();
            deleteColumn.Text = "Delete";
            deleteColumn.Name = "";
            deleteColumn.Width = 100;
            deleteColumn.UseColumnTextForButtonValue = true;
            deleteColumn.UseColumnTextForButtonValue = true;
            deleteColumn.FlatStyle = FlatStyle.Flat;
            deleteColumn.DefaultCellStyle.ForeColor = Color.Red;
            deleteColumn.DefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Underline);
            dataGridView2.Columns.Add(deleteColumn);

            //Subscribe to the CellClick event of the DataGridView for delete button.
            dataGridView2.CellClick += async (sender, e) =>
            {
                // Check if the clicked cell is in the "Delete" button column
                if (e.ColumnIndex == deleteColumn.Index && e.RowIndex >= 0)
                {
                    // Get the BackUpFileDto associated with the clicked row
                    if (dataGridView2.Rows[e.RowIndex].DataBoundItem is BackUpFileDto selectedBackupFile)
                    {
                        // Show a confirmation message before deleting the file
                        DialogResult result = MessageBox.Show(detailsForm, $"Do you want to delete {selectedBackupFile.Filename}?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            try
                            {
                                await _presenter.DeleteBackUpFile(selectedBackupFile); // Delete the backup file         
                            }

                            catch (Exception)
                            {
                                MessageBox.Show(detailsForm, $"An error has occured, please try again.", "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                            finally
                            {
                                // Refresh the DataGridView
                                relatedBackupFiles.Remove(selectedBackupFile);
                                dataGridView2.DataSource = null;
                                dataGridView2.DataSource = relatedBackupFiles;

                                // DisplayIndex beginning from DocumentVersion
                                dataGridView2.Columns["DocumentVersion"].DisplayIndex = 0;
                                dataGridView2.Columns["Filename"].DisplayIndex = 1;
                                dataGridView2.Columns["BackupDate"].DisplayIndex = 2;
                                dataGridView2.Columns["Version"].DisplayIndex = 3;

                                // Display Columns in datagridview2
                                dataGridView2.Columns["DocumentVersion"].Width = 170;
                                dataGridView2.Columns["Filename"].Width = 330;
                                dataGridView2.Columns["BackupDate"].Width = 120;
                                dataGridView2.Columns["Version"].Width = 100;

                                dataGridView2.Columns["DocumentVersion"].HeaderText = "Document No.";
                                dataGridView2.Columns["Filename"].HeaderText = "Document Title";
                                dataGridView2.Columns["BackupDate"].HeaderText = "Date";
                                dataGridView2.Columns["Version"].HeaderText = "Version No.";

                                dataGridView2.Columns["BackupId"].Visible = false;
                                dataGridView2.Columns["OriginalFilePath"].Visible = false;
                                dataGridView2.Columns["BackupFilePath"].Visible = false;
                                dataGridView2.Columns["Id"].Visible = false;
                            }
                        }
                    }
                }
            };

            #endregion

            #region Details Form Buttons
            // Create the Edit button
            CustomButton editButton = new CustomButton(ColorTranslator.FromHtml("#576CBC"), SystemColors.Control);
            editButton.FlatAppearance.BorderSize = 0;
            editButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(9, 142, 154);
            editButton.FlatStyle = FlatStyle.Flat;
            editButton.Text = "Edit";
            editButton.Name = "editButton";
            editButton.Location = new Point(groupBox.Right - editButton.Width, groupBox.Bottom + 10);
            editButton.Height = 40;
            editButton.Width = 80;
            editButton.Visible = isAdmin; // Enable for admin only
            editButton.Click += EditButton_Click;
            detailsForm.Controls.Add(editButton);

            // Create the Close button
            CustomButton closeButton = new CustomButton(ColorTranslator.FromHtml("#DA0B0B"), SystemColors.Control);
            closeButton.FlatAppearance.BorderSize = 0;
            closeButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(9, 142, 154);
            closeButton.FlatStyle = FlatStyle.Flat;
            closeButton.Text = "Close";
            closeButton.Name = "closeButton";
            closeButton.Location = new Point(editButton.Left - 20 - closeButton.Width, groupBox.Bottom + 10);
            closeButton.Location = isAdmin ? new Point(editButton.Left - 20 - closeButton.Width, groupBox.Bottom + 10) : new Point(editButton.Left - 10, groupBox.Bottom + 10);
            closeButton.Height = editButton.Height;
            closeButton.Width = editButton.Width;
            closeButton.Click += CloseButton_Click;
            detailsForm.Controls.Add(closeButton);


            // Create the Save button using the custom button class
            CustomButton saveButton = new CustomButton(ColorTranslator.FromHtml("#05982E"), SystemColors.Control);
            saveButton.FlatAppearance.BorderSize = 0;
            saveButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(9, 142, 154);
            saveButton.FlatStyle = FlatStyle.Flat;
            saveButton.Text = "Save";
            saveButton.Name = "saveButton";
            saveButton.Location = new Point(closeButton.Left - 20 - saveButton.Width, closeButton.Top);
            saveButton.Height = closeButton.Height;
            saveButton.Width = closeButton.Width;
            saveButton.Visible = isAdmin;
            saveButton.Enabled = false; // Disable the Save button initially

            // Create a dictionary to store the original values of the TextBoxes
            var originalTextBoxValues = new Dictionary<TextBox, string>();
            var originalComboBoxValues = new Dictionary<ComboBox, string?>();

            // Save button click event
            saveButton.Click += async (sender, e) =>
            {
                try
                {
                    // Check if a file has been uploaded
                    string? filePath = filenameTextBox.Tag as string;

                    // Read the file data from the selected file if a new file has been uploaded
                    byte[] fileDataBytes = isNewFileUploaded ? File.ReadAllBytes(filePath) : selectedDocument.FileData;

                    // Get the modified data from the TextBoxes and ComboBoxes
                    string documentVersion = documentVersionTextBox.Text.ToUpper().Trim();
                    string filename = filenameTextBox.Text.Trim();
                    string filenameExtension = selectedDocument.FilenameExtension;
                    string category = categoryComboBox.Text;
                    string status = statusComboBox.Text;
                    DateTime createdDate = DateTime.Parse(createdDateTextBox.Text);
                    string createdBy = createdByTextBox.Text;
                    string modifiedBy = labelHomePageUserLogin.Text;
                    string notes = notesTextBox.Text.Trim();

                    // Check if the file name has been changed
                    if (filename != Path.GetFileNameWithoutExtension(filePath))
                    {
                        filenameTextBox.Text = filename;
                    }

                    // Create a new DocumentDto with the modified data
                    DocumentDto modifiedDocument = new DocumentDto
                    {
                        Id = selectedDocument.Id,
                        DocumentVersion = documentVersion,
                        Filename = filename,
                        Category = category,
                        Status = status,
                        CreatedDate = createdDate,
                        CreatedBy = createdBy,
                        ModifiedBy = modifiedBy,
                        ModifiedDate = DateTime.Now,
                        Notes = notes,
                        FilenameExtension = filenameExtension
                    };

                    _presenter.EditDocument(modifiedDocument, fileDataBytes, isUploadSuccessful); // Edit the document in the database

                    await _presenter.LoadDocumentsByFilter(GetSearchQuery(), GetFilterCategory()); // Load the filtered documents again to update the view

                    var result = MessageBox.Show(detailsForm, $"{filenameTextBox.Text} details have been updated.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); // Show a message to indicate that the document has been updated.                                                                                                                                                   
                    if (result == DialogResult.OK)
                    {
                        detailsForm.Close(); // Close the detailsForm
                    }

                    // After saving, make the TextBoxes read-only again
                    foreach (Control control in groupBox.Controls)
                    {
                        if (control is TextBox textBox)
                        {
                            textBox.ReadOnly = true;
                            textBox.TextChanged -= TextBox_TextChanged; // Detach the event handler to stop tracking changes
                        }
                        else if (control is ComboBox comboBox)
                        {
                            comboBox.Enabled = false; // Disable the ComboBox controls
                            comboBox.SelectedIndexChanged -= ComboBox_SelectedIndexChanged; // Detach the event handler to stop tracking changes
                        }
                    }
                }


                catch (Exception)
                {
                    MessageBox.Show($"An unexpected error occurred while saving the document. Please try again or contact support.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                finally
                {
                    isNewFileUploaded = false; // Reset the flag
                    isUploadSuccessful = false; // Reset the flag
                    saveButton.Enabled = false; // Disable the Save button after save
                    editButton.Enabled = true; // Re-enable the Edit button after save
                }


            };
            detailsForm.Controls.Add(saveButton);

            // Handle the Edit button click event
            editButton.Click += (sender, e) =>
            {
                // Enable editing of the controls inside the GroupBox.
                foreach (Control control in groupBox.Controls)
                {
                    if (control is TextBox textBox)
                    {
                        originalTextBoxValues[textBox] = textBox.Text; // Store the original value of the TextBox
                        textBox.ReadOnly = false;
                        textBox.TextChanged += TextBox_TextChanged; // Attach the event handler to track changes
                    }

                    else if (control is ComboBox comboBox)
                    {
                        originalComboBoxValues[comboBox] = comboBox.SelectedItem.ToString();
                        comboBox.Enabled = true; // Enable the ComboBox controls
                        comboBox.SelectedIndexChanged += ComboBox_SelectedIndexChanged; // Attach the event handler to track changes
                    }
                }

                uploadFileButton.Enabled = true;
                editButton.Enabled = false;
            };

            // Adjust the size of the detailsForm to fit the GroupBox and its contents
            detailsForm.ClientSize = new Size(groupBox.Right + 40, groupBox.Bottom + 80);

            // Handle the Click event of Button 1
            button1.Click += (sender, e) =>
            {
                groupBox.Visible = true;
                editButton.Visible = true;
                saveButton.Visible = true;
                closeButton.Visible = true;
            };

            // Handle the Click event of Button 2
            button2.Click += (sender, e) =>
            {
                groupBox.Visible = false;
                groupBox2.Visible = true;
                editButton.Visible = false;
                saveButton.Visible = false;
                closeButton.Visible = false;
            };

            // Handle the Edit button click event
            void EditButton_Click(object? sender, EventArgs e)
            {
                foreach (Control control in groupBox.Controls)
                {
                    if (control is TextBox textBox)
                    {
                        filenameTextBox.Enabled = true;
                        documentVersionTextBox.Enabled = true;
                        notesTextBox.Enabled = true;
                        createdDateTextBox.Enabled = false;
                        createdByTextBox.Enabled = false;
                        modifiedByTextBox.Enabled = false;
                        modifiedDateTextBox.Enabled = false;

                        createdByTextBox.BorderStyle = BorderStyle.None;
                        createdDateTextBox.BorderStyle = BorderStyle.None;
                        modifiedByTextBox.BorderStyle = BorderStyle.None;
                        modifiedDateTextBox.BorderStyle = BorderStyle.None;
                    }
                    else if (control is ComboBox comboBox)
                    {
                        statusComboBox.Enabled = true;
                        categoryComboBox.Enabled = true;
                    }
                }

                editButton.Enabled = false;
            }

            // Handle the Close button click event
            void CloseButton_Click(object? sender, EventArgs e)
            {
                detailsForm.Close(); // Close the form when the Close button is clicked.
            }

            void TextBox_TextChanged(object? sender, EventArgs e)
            {
                // Enable the Save button when changes are made in any of the TextBoxes
                //saveButton.Enabled = true;
                errorProvider.Clear();

                int maxDocNameLength = 100;
                int maxtextBoxDocumentVersionLength = 30;
                int mintextBoxDocumentVersionLength = 5;
                int maxtxtBoxNotesLength = 150;

                // Check if the value in the TextBox has been reverted to the original value
                if (sender is TextBox textBox && originalTextBoxValues.ContainsKey(textBox))
                {
                    if (textBox.Text == originalTextBoxValues[textBox])
                    {
                        saveButton.Enabled = false; // If the current value matches the original value, disable the Save button
                    }

                    else if (filenameTextBox.Text.Length > maxDocNameLength)
                    {
                        errorProvider.SetError(filenameTextBox, $"Document name must be {maxDocNameLength} characters or less.");
                        saveButton.Enabled = false;
                    }

                    else if (documentVersionTextBox.Text.Trim().Length < mintextBoxDocumentVersionLength || documentVersionTextBox.Text.Length > maxtextBoxDocumentVersionLength)
                    {
                        errorProvider.SetError(documentVersionTextBox, $"Document No. must be within {mintextBoxDocumentVersionLength} and {maxtextBoxDocumentVersionLength} characters.");
                        saveButton.Enabled = false;
                    }

                    else if (notesTextBox.Text.Trim().Length >= maxtxtBoxNotesLength)
                    {
                        errorProvider.SetError(notesTextBox, $"Notes must be {maxtxtBoxNotesLength} characters or less.");
                        saveButton.Enabled = false;
                    }

                    else
                    {
                        saveButton.Enabled = true;
                    }

                }
            }

            // ComboBox SelectedIndexChanged event handler to track changes
            void ComboBox_SelectedIndexChanged(object? sender, EventArgs e)
            {
                saveButton.Enabled = true; // Enable the Save button when changes are made in any of the ComboBoxes

                // Check if the selected value in the ComboBox has been reverted to the original value
                if (sender is ComboBox comboBox && originalComboBoxValues.ContainsKey(comboBox))
                {
                    if (comboBox.SelectedItem.ToString() == originalComboBoxValues[comboBox])
                    {
                        saveButton.Enabled = false; // If the current value matches the original value, disable the Save button
                    }
                }
            }

            detailsForm.ShowDialog(); // Show the detailsForm

            #endregion
        }

        private void ConfirmDownloadDocument(DocumentDto selectedDocument, bool isAdmin)
        {
            try
            {
                // Get the file data (byte array) from the selectedDocument
                byte[] fileData = selectedDocument.FileData;

                // Distination path in the user's "Downloads" folder
                string downloadsPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string destinationFilePath = Path.Combine(downloadsPath, "Downloads", selectedDocument.Filename + selectedDocument.FilenameExtension);

                string fileExtension = selectedDocument.FilenameExtension;

                // Check user access prviledges as admin
                if (isAdmin)
                {
                    // For other file types, simply write the file data to the destination file
                    File.WriteAllBytes(destinationFilePath, fileData);
                }

                else
                {
                    // For PDF files, save the file data directly
                    if (fileExtension == ".pdf")
                    {
                        File.WriteAllBytes(destinationFilePath, fileData);
                    }

                    else if (fileExtension == ".docx" || fileExtension == ".doc")
                    {
                        // Save the file data to a temporary file
                        string tempFilePath = Path.GetTempFileName();
                        File.WriteAllBytes(tempFilePath, selectedDocument.FileData);

                        // If it's a Word document, convert it to PDF using Office Interop
                        Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
                        Microsoft.Office.Interop.Word.Document doc = word.Documents.Open(tempFilePath);

                        // Specify the destination file with a ".pdf" extension
                        string pdfFilePath = Path.ChangeExtension(destinationFilePath, ".pdf");

                        doc.SaveAs2(pdfFilePath, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF);
                        doc.Close();
                        word.Quit();

                        File.Delete(tempFilePath); // Delete the temporary file
                    }

                    else if (fileExtension == ".xlsx" || fileExtension == ".xls")
                    {
                        // Save the file data to a temporary file
                        string tempFilePath = Path.GetTempFileName();
                        File.WriteAllBytes(tempFilePath, selectedDocument.FileData);

                        // If it's an Excel document, convert it to PDF using Office Interop
                        Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                        Microsoft.Office.Interop.Excel.Workbook wb = excel.Workbooks.Open(tempFilePath);

                        string pdfFilePath = Path.ChangeExtension(destinationFilePath, ".pdf"); // Specify the destination file with a ".pdf" extension

                        wb.ExportAsFixedFormat(Microsoft.Office.Interop.Excel.XlFixedFormatType.xlTypePDF, pdfFilePath);
                        wb.Close();
                        excel.Quit();

                        File.Delete(tempFilePath); // Delete the temporary file
                    }

                    else
                    {
                        MessageBox.Show("An error occurred while downloading the document. Please try again.", "Download Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }

                MessageBox.Show($"{selectedDocument.Filename} downloaded successfully!", "Download Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            catch (Exception)
            {
                MessageBox.Show("An error occurred while downloading the document. Please try again.", "Download Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UploadFileButton_Click(object? sender, EventArgs e, TextBox filenameTextBox)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "All files (*.*)|*.*";
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName; // Get the full path of the selected file
                    string filePathExtension = Path.GetExtension(filePath).ToLower();

                    // Check if the selected file has a valid extension (docx, doc, pdf, xlsx, xls)
                    if (IsFileExtensionValid(filePathExtension))
                    {
                        string filePathFilename = Path.GetFileNameWithoutExtension(filePath);

                        // Check if the selected file is different from the current file
                        if (!string.Equals(filePathFilename, filenameTextBox.Text, StringComparison.OrdinalIgnoreCase))
                        {
                            filenameTextBox.Text = filePathFilename;
                            labelFilenameHidden.Text = filePathExtension;
                            isNewFileUploaded = true; // Set the flag to indicate that a new file has been uploaded
                        }
                        else
                        {
                            MessageBox.Show($"{filenameTextBox.Text} already exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); // Show an error message if the selected file is the same as the current file
                        }

                        filenameTextBox.Enabled = false; // Disable editing of the TextBox

                        filenameTextBox.Tag = filePath; // Store the file path in the Tag property of the TextBox
                    }
                    else
                    {
                        MessageBox.Show("Invalid document type. Please select a Word, PDF, or Excel document.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    isUploadSuccessful = true;
                }

                else
                {
                    isUploadSuccessful = false;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred while selecting the document. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        // Function to check if the file extension is valid
        private bool IsFileExtensionValid(string extension)
        {
            // List of valid file extensions (docx, doc, pdf, xlsx, xls)
            List<string> validExtensions = new List<string> { ".docx", ".doc", ".pdf", ".xlsx", ".xls" };
            return validExtensions.Contains(extension);
        }

        // Add document button click event
        private async void pictureBox1_Click(object sender, EventArgs e)
        {
            using (AddFormView newForm = new AddFormView(_unitOfWork, _presenter))
            {
                newForm.StartPosition = FormStartPosition.CenterParent;
                newForm.ShowDialog();
            }

            await _presenter.LoadDocumentsByFilter(GetSearchQuery(), GetFilterCategory()); // Load documents including newly added
        }

        public void ShowDocumentView() => this.ShowDialog(); // Show documentMainView form

        private void DocumentViewForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit(); // Exit the application when the form is closed
            }
        }

        #endregion

        #region Manage Users Methods

        // Event method when details button was clicked
        public void ToggleAdminRights(bool isVisible)
        {
            buttonManageUsers.Visible = isVisible;
            pictureBox1.Visible = isVisible; // Add document icon
            labelDownloadAllDocs.Visible = isVisible;
            linkLabelDownloadAllDocs.Visible = isVisible;
        }

        // event when details button was clicked
        private void dataGridViewManageUsers_DetailsButton_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridViewManageUsers.Columns["Details"].Index)
            {
                UserCredentialsDto selectedUser = (UserCredentialsDto)dataGridViewManageUsers.Rows[e.RowIndex].DataBoundItem;

                panelUserDetails.Visible = true;
                panelManageUsers.Visible = false;
                panelHome.Visible = false;
                panelDocumentButton.Visible = false;

                ShowUserDetails(selectedUser); // Show the selected user's details
            }
        }

        private void ShowUserDetails(UserCredentialsDto selectedUser)
        {
            // Display the selected user's details in the textboxes
            textBoxID.Text = selectedUser.UserId.ToString();
            textBoxUserFirstName.Text = selectedUser.Firstname;
            textBoxUserLastName.Text = selectedUser.Lastname;
            textBoxUserEmailAdd.Text = selectedUser.EmailAddress;
            textBoxUserJobTitle.Text = selectedUser.JobTitle;
            checkBoxEnableAdmin.Checked = selectedUser.UserRole == UserRole.Admin;

            // Field status
            textBoxID.Enabled = false;
            textBoxUserFirstName.Enabled = false;
            textBoxUserLastName.Enabled = false;
            textBoxUserEmailAdd.Enabled = false;
            textBoxUserJobTitle.Enabled = false;
            checkBoxEnableAdmin.Enabled = false;

            // Manage Users button state initially
            buttonUsersDetailSave.Enabled = false;
            buttonUsersDetailSave.BackColor = SystemColors.Control;

            buttonEditUser.Enabled = true;
            buttonEditUser.BackColor = ColorTranslator.FromHtml("#A5D7E8");

            buttonCloseUser.Enabled = true;
            buttonCloseUser.BackColor = ColorTranslator.FromHtml("#DA0B0B");
        }

        private Dictionary<Control, string> originalValues = new Dictionary<Control, string>();

        private void buttonEditUser_Click(object sender, EventArgs e)
        {
            buttonEditUser.Enabled = false; // Disable the Edit button
            buttonCloseUser.Enabled = true; // Disable the Close button

            // Enable editing of the textboxes
            textBoxID.Enabled = false;
            checkBoxEnableAdmin.Enabled = true;
            textBoxUserFirstName.Enabled = true;
            textBoxUserLastName.Enabled = true;
            textBoxUserEmailAdd.Enabled = true;
            textBoxUserJobTitle.Enabled = true;

            // Store the original values of the text fields in the Dictionary
            originalValues.Clear(); // Clear any previous values
            originalValues.Add(textBoxUserFirstName, textBoxUserFirstName.Text);
            originalValues.Add(textBoxUserLastName, textBoxUserLastName.Text);
            originalValues.Add(textBoxUserEmailAdd, textBoxUserEmailAdd.Text);
            originalValues.Add(textBoxUserJobTitle, textBoxUserJobTitle.Text);

            // Store the original state of the checkbox
            originalCheckBoxState = checkBoxEnableAdmin.Checked;
        }

        private async void buttonUsersDetailSave_Click(object sender, EventArgs e)
        {
            try
            {
                string varUserSearchQuery = GetSearchUserQuery();
                string varUserJobcatergoryQuery = GetFilterUsersCategory();

                // Get modified data from the textboxes
                int userId = int.Parse(textBoxID.Text);
                UserRole userRole = checkBoxEnableAdmin.Checked ? UserRole.Admin : UserRole.User;
                string firstname = textBoxUserFirstName.Text;
                string lastname = textBoxUserLastName.Text;
                string emailAddress = textBoxUserEmailAdd.Text;
                string jobTitle = textBoxUserJobTitle.Text;

                // create new user object from the modified data
                UserCredentialsDto modifiedUser = new UserCredentialsDto
                {
                    UserId = userId,
                    UserRole = userRole,
                    Firstname = firstname,
                    Lastname = lastname,
                    EmailAddress = emailAddress,
                    JobTitle = jobTitle
                };

                _presenter.EditUser(modifiedUser); // Edit the user in the database     

                await _presenter.LoadUsersByFilter(varUserSearchQuery, varUserJobcatergoryQuery); // Load the user by filter
            }

            catch (Exception)
            {
                MessageBox.Show("An error occurred while saving users details. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                // Return to Manage Users page
                panelUserDetails.Visible = false;
                panelManageUsers.Visible = true;
                panelHome.Visible = false;
                panelDocumentButton.Visible = false;

                // Clear the original values dictionary
                originalValues.Clear();

                // Reset the original checkbox state
                originalCheckBoxState = checkBoxEnableAdmin.Checked;

                // Disable the Save button after saving
                buttonUsersDetailSave.Enabled = false;
            }
        }

        private void buttonCloseUser_Click(object sender, EventArgs e)
        {
            // close panelUserDetails
            panelUserDetails.Visible = false;
            panelManageUsers.Visible = true;
        }

        // event when user fields was changed
        private void TextBoxUsers_TextChanged(object? sender, EventArgs e)
        {
            Control textBox = (Control)sender;
            string? originalValue;

            if (originalValues.TryGetValue(textBox, out originalValue))
            {
                string currentValue = textBox.Text;

                // Check if the text has changed
                if (originalValue != currentValue)
                {
                    // Enable the Save button when changes are detected
                    buttonUsersDetailSave.Enabled = true;
                    buttonUsersDetailSave.BackColor = ColorTranslator.FromHtml("#05982E");
                }
                else
                {
                    // Disable the Save button when there are no changes
                    buttonUsersDetailSave.Enabled = false;
                    buttonUsersDetailSave.BackColor = SystemColors.Control;
                }
            }
        }

        private bool originalCheckBoxState;

        private void CheckBox_CheckedChanged(object? sender, EventArgs e)
        {
            // Enable the Save button when the checkbox state changes
            if (originalCheckBoxState != checkBoxEnableAdmin.Checked)
            {
                buttonUsersDetailSave.Enabled = true;
                buttonUsersDetailSave.BackColor = ColorTranslator.FromHtml("#05982E");
            }
            else
            {
                // Disable the Save button when the checkbox state is the same as the original
                buttonUsersDetailSave.Enabled = false;
                buttonUsersDetailSave.BackColor = SystemColors.Control;
            }
        }

        private void checkBoxEnableAdmin_CheckedChanged(object sender, EventArgs e) { }

        // event when user delete button was clicked
        private async void dataGridViewManageUsers_DeleteButton_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridViewManageUsers.Columns["Delete"].Index)
            {
                UserCredentialsDto selectedUser = (UserCredentialsDto)dataGridViewManageUsers.Rows[e.RowIndex].DataBoundItem;
                // Show the delete confirmation modal directly in the main form.
                DialogResult result = MessageBox.Show($"Do you want to remove {selectedUser.UserName} as user?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                try
                {
                    if (result == DialogResult.Yes)
                    {
                        var currentUsersSearchQueryWhenItemDeleted = GetSearchUserQuery();
                        var currentUsersFilterCategoryWhenItemDeleted = GetFilterUsersCategory();

                        await _presenter.DeleteUser(selectedUser);
                        await _presenter.LoadUsersByFilter(currentUsersSearchQueryWhenItemDeleted, currentUsersFilterCategoryWhenItemDeleted);
                    }
                }

                catch (Exception)
                {
                    MessageBox.Show("An error occurred while deleting the user. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        #endregion

        #region Document Filter and Pagination Functionalities

        private void textBoxSearchBar_TextChanged(object? sender, EventArgs e)
        {
            timerSearchBar.Interval = 400; // Set the interval for the document searchbar timer
            timerSearchBar.Start();
        }

        private void comboBoxCategoryDropdown_SelectedIndexChanged(object? sender, EventArgs e)
        {
            timerDocsCategory.Interval = 100;
            timerDocsCategory.Start();
        }
        private void ApplyFilters() => _presenter.ApplyFilters(); // Call presenter's ApplyFilters method

        private void pictureBox3_Click(object? sender, EventArgs e)
        {
            _presenter.NextPage(); // Call presenter's NextPage method
        }

        private void iconBack_Click(object? sender, EventArgs e) => _presenter.PreviousPage(); // Call presenter's PreviousPage method

        // Implement the IMainDocumentView interface methods
        public string GetSearchQuery() => textBoxSearchBar.Text.Trim() ?? string.Empty; // Method for search bar query

        public string GetFilterCategory() => comboBoxCategoryDropdown.SelectedItem?.ToString() ?? string.Empty; // Method for combo box query

        // Method to update the page label for document pagination
        public void UpdatePageLabel(int currentPage, int totalPages)
        {
            // Add condition if totalPages is 0
            if (totalPages == 0)
            {
                labelDocumentPagination.Text = $"Page {currentPage} of {totalPages + 1}";
            }

            else if (totalPages < currentPage && totalPages > 0)
            {
                labelDocumentPagination.Text = $"Page {currentPage - 1} of {totalPages}";
            }

            else
            {
                labelDocumentPagination.Text = $"Page {currentPage} of {totalPages}";
            }

            // Disable back icon if page == 1
            iconBack.Enabled = currentPage <= 1 || totalPages <= 1 ? false : true;

            // Disable next ixon if page == totalpages
            iconNext.Enabled = currentPage == totalPages || totalPages <= 1 ? false : true;
        }

        #endregion

        #region Users Filter and Pagination Functionalities
        private void textBoxUsersSearchBox_TextChanged(object? sender, EventArgs e)
        {
            timerUserSearchBar.Interval = 400;
            timerUserSearchBar.Start();
        }

        private void comboBox_JobCategory_SelectedIndexChanged(object? sender, EventArgs e) => ApplyUsersPageFilters(); // Apply  users filter when the selected index changes

        private void ApplyUsersPageFilters() => _presenter.ApplyUsersPageFilters(); // Call presenter's ApplyUsersPageFilters method 

        private void pictureBoxUsersNextIcon_Click(object? sender, EventArgs e) => _presenter.NextUsersPage(); // Call presenter's NextUsersPage method

        private void pictureBoxUsersBackIcon_Click(object? sender, EventArgs e) => _presenter.BackUsersPage(); // Call presenter's BackUsersPage method

        public string GetSearchUserQuery() => textBoxUsersSearchBox.Text.Trim() ?? string.Empty; // Method for search bar query

        public string GetFilterUsersCategory() => comboBox_JobCategory.SelectedItem?.ToString() ?? string.Empty; // Method for combo box query


        public void UpdateUsersPageLabel(int currentPageUsers, int UsersTotalPages)
        {
            // add condition where if UsersTotalPages is 0
            if (UsersTotalPages == 0)
            {
                labelUsersPagination.Text = $"Page {currentPageUsers} of {UsersTotalPages + 1}";
            }

            else if (UsersTotalPages < currentPageUsers && UsersTotalPages > 0)
            {
                labelUsersPagination.Text = $"Page {currentPageUsers - 1} of {UsersTotalPages}";
            }

            else
            {
                labelUsersPagination.Text = $"Page {currentPageUsers} of {UsersTotalPages}";
            }


            // Disable back icon if page == 1
            pictureBoxUsersBackIcon.Enabled = currentPageUsers <= 1 || UsersTotalPages <= 1 ? false : true;

            // Disable next icon if page == totalpages
            pictureBoxUsersNextIcon.Enabled = currentPageUsers == UsersTotalPages || UsersTotalPages <= 1 ? false : true;


        }

        #endregion

        // Implement the SetUsernameLabel method from the iDocument interface
        public void SetUsernameLabel(string username)
        {
            labelHomePageUserLogin.Text = username;
            labelHello.Text = $"Hello, {username}!";
        }

        // Download all documents click event
        private async void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to download all documents?", "Download Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                await _presenter.DownloadAllDocuments();
            }
        }

        // Button Sign out onclick event
        private void buttonSignOut_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show($"Hello {labelHomePageUserLogin.Text}, do you want to sign out?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during sign-out: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void timerSearchBar_Tick(object? sender, EventArgs e)
        {
            timerSearchBar.Stop(); // Stop timer after interval
            ApplyFilters();
        }
        private void timerUserSearchBar_Tick(object sender, EventArgs e)
        {
            timerUserSearchBar.Stop();
            ApplyUsersPageFilters();
        }

        private void timerDocsCategory_Tick(object sender, EventArgs e)
        {
            timerDocsCategory.Stop();
            ApplyFilters();
        }
    }
}
