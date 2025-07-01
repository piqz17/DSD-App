using DSD_WinformsApp.Core.DTOs;
using DSD_WinformsApp.Infrastructure.Data;
using DSD_WinformsApp.Presenter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DSD_WinformsApp.View
{
    public partial class AddFormView : Form
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDocumentPresenter _presenter;

        private ErrorProvider errorProvider = null!; // Class-level variable to store the ErrorProvider component

        private string selectedFilePath = null!; // Class-level variable to store the selected file path

        private string labelFilenameText = "";

        public AddFormView(IUnitOfWork unitOfWork, IDocumentPresenter presenter)
        {
            InitializeComponent();
            _unitOfWork = unitOfWork;
            _presenter = presenter;

            MaximizeBox = false; // Remove the maximize box
            MinimizeBox = false; // Remove the minimize box

            errorProvider = new ErrorProvider();  // Initialize the ErrorProvider component

            // Initialize the ComboBox controls
            StatusComboBox();
            CategoryComboBox();
            CreatedByComboBox();
        }

        private void AddForm_Load(object sender, EventArgs e)
        {
            btnSave.Enabled = false; // Disable the Save button initially

            labelFilename.Visible = false; // Hide the label that displays the selected file name

            errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink; // remove the blinking icon when error occurs

            // Set the background color of buttons initially.
            btnSave.BackColor = SystemColors.Control;
            buttonUploadDocs.BackColor = ColorTranslator.FromHtml("#A5D7E8");
            btnCancel.BackColor = ColorTranslator.FromHtml("#DA0B0B");

            // Attach SelectedIndexChanged event handlers to ComboBox controls
            cmbCategories.SelectedIndexChanged += Control_SelectedIndexChanged;
            comboBoxCreatedBy.SelectedIndexChanged += Control_SelectedIndexChanged;
            cmbStatus.SelectedIndexChanged += Control_SelectedIndexChanged;

            // Attach TextChanged event handlers to relevant controls
            labelFilename.TextChanged += Control_TextChanged;
            textBoxDocumentVersion.TextChanged += Control_TextChanged;
            txtBoxNotes.TextChanged += Control_TextChanged;
        }

        // Status selection
        private void StatusComboBox()
        {
            cmbStatus.Items.Add("New");
            cmbStatus.Items.Add("Revised");
            cmbStatus.Items.Add("Obsolete");
        }

        // Category selection
        private void CategoryComboBox()
        {
            cmbCategories.Items.Add("Board Resolutions");
            cmbCategories.Items.Add("Canteen Policies");
            cmbCategories.Items.Add("COOP Policies");
            cmbCategories.Items.Add("COOP Article & By Laws");
            cmbCategories.Items.Add("Minutes of the Meeting");
            cmbCategories.Items.Add("Regulatory Requirements");
        }

        // Add method for combo box items from the database
        private async void CreatedByComboBox()
        {
            var users = await _presenter.GetAllRegisteredUsers();
            foreach (var user in users)
            {
                string fullName = user.Firstname + " " + user.Lastname;
                comboBoxCreatedBy.Items.Add(fullName);
            }
        }


        private void btnSave_Click(object? sender, EventArgs e)
        {
            try
            {
                // Validate filePath 
                if (string.IsNullOrEmpty(selectedFilePath))
                {
                    MessageBox.Show("Please select a document before saving.");
                    return;
                }

                byte[] fileDataBytes = File.ReadAllBytes(selectedFilePath);

                var documentDto = new DocumentDto
                {
                    Filename = labelFilenameText,
                    FilenameExtension = labelDocumentNameWithExtension.Text,
                    DocumentVersion = textBoxDocumentVersion.Text.ToUpper(),
                    Category = cmbCategories.SelectedItem?.ToString() ?? "",
                    Status = cmbStatus.SelectedItem?.ToString() ?? "",
                    Notes = txtBoxNotes.Text,
                    CreatedBy = comboBoxCreatedBy.SelectedItem?.ToString() ?? "",
                    CreatedDate = DateTime.Now.Date,
                };

                _presenter.SaveDocument(documentDto, fileDataBytes); // Save added document to database

                _presenter.AddNewDocument(documentDto); // Reflect added document in documentview
            }
            catch (Exception)
            {
                MessageBox.Show("An unexpected error occurred while saving the document. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                DialogResult = DialogResult.OK; // Set DialogResult in the finally block
            }
        }

        // Cancel button onclick event
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private async void buttonUploadDocs_Click(object? sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "All Files|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedExtension = Path.GetExtension(openFileDialog.FileName); // Get the selected file extension
                    List<string> allowedExtensions = new List<string> { ".docx", ".doc", ".xlsx", ".xls", ".pdf" }; // Allowed file extensions
                    if (!allowedExtensions.Contains(selectedExtension))
                    {
                        MessageBox.Show("Invalid document type. Please select a Word, PDF, or Excel document.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    selectedFilePath = openFileDialog.FileName; // Store the selected file path

                    // Display only the file name without the extension in the label and the TextBox
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(openFileDialog.FileName);

                    // Limit characters per line for filename
                    string dynamicText = fileNameWithoutExtension;
                    int maxCharactersPerLine = 50;
                    string firstLine = dynamicText.Length > maxCharactersPerLine ? dynamicText.Substring(0, maxCharactersPerLine) : dynamicText;
                    string secondLine = dynamicText.Length > maxCharactersPerLine ? dynamicText.Substring(maxCharactersPerLine) : string.Empty;

                    labelFilename.Text = dynamicText.Length > maxCharactersPerLine ? $"{firstLine}\n{secondLine}" : dynamicText;
                    labelFilenameText = labelFilename.Text.Replace("\n", "");

                    // Get filename with extension for saving to db
                    string fileExtension = Path.GetExtension(openFileDialog.FileName);
                    labelDocumentNameWithExtension.Text = fileExtension;

                    bool hasDuplicateFileName = await _presenter.CheckForDuplicateFileName(fileNameWithoutExtension); // Check for duplicate file name in the repository

                    if (hasDuplicateFileName)
                    {
                        MessageBox.Show($"{fileNameWithoutExtension} already exists. Please rename the document.", "Duplicate File", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    labelFilename.Visible = true; // show document label
                }
            }

            catch (Exception)
            {
                MessageBox.Show("An unexpected error occurred while selecting a document. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Control_TextChanged(object? sender, EventArgs e)
        {
            ValidateForm();
        }

        private void Control_SelectedIndexChanged(object? sender, EventArgs e)
        {
            ValidateForm();
        }

        private bool isProcessingValidation = false;

        private void ValidateForm()
        {
            // Check validation if already running to avoid recursion.
            if (isProcessingValidation)
            {
                return;
            }

            isProcessingValidation = true;

            try
            {
                bool isValid = true;
                errorProvider.Clear();

                int maxLabelFilenameLength = 100; // Set filename validation

                if (string.IsNullOrWhiteSpace(labelFilename.Text))
                {
                    errorProvider.SetError(buttonUploadDocs, "Upload document is required.");
                    isValid = false;
                }

                else if (labelFilename.Text.Trim().Length > maxLabelFilenameLength)
                {
                    errorProvider.SetError(buttonUploadDocs, $"Uploaded document name must be {maxLabelFilenameLength} characters or less.");
                    isValid = false;
                }

                // Document version field validations
                int maxtextBoxDocumentVersionLength = 30;
                int mintextBoxDocumentVersionLength = 5;

                if (string.IsNullOrWhiteSpace(textBoxDocumentVersion.Text))
                {
                    errorProvider.SetError(textBoxDocumentVersion, "Document Version is required.");
                    isValid = false;
                }

                else if (textBoxDocumentVersion.Text.Trim().Length < mintextBoxDocumentVersionLength || textBoxDocumentVersion.Text.Length > maxtextBoxDocumentVersionLength)
                {
                    errorProvider.SetError(textBoxDocumentVersion, $"Document No. must be within {mintextBoxDocumentVersionLength} and {maxtextBoxDocumentVersionLength} characters.");
                    isValid = false;
                }

                if (cmbCategories.SelectedItem == null)
                {
                    errorProvider.SetError(cmbCategories, "Document Category is required.");
                    isValid = false;
                }

                if (cmbStatus.SelectedItem == null)
                {
                    errorProvider.SetError(cmbStatus, "Status is required.");
                    isValid = false;
                }

                if (comboBoxCreatedBy.SelectedItem == null)
                {
                    errorProvider.SetError(comboBoxCreatedBy, "Created by is required.");
                    isValid = false;
                }

                // Notes field validation
                int maxtxtBoxNotesLength = 150;

                if (string.IsNullOrWhiteSpace(txtBoxNotes.Text))
                {
                    errorProvider.SetError(txtBoxNotes, "Notes are required.");
                    isValid = false;
                }

                else if (txtBoxNotes.Text.Trim().Length >= maxtxtBoxNotesLength)
                {
                    errorProvider.SetError(txtBoxNotes, $"Notes must be {maxtxtBoxNotesLength} characters or less.");
                    isValid = false;
                }

                // Enable or disable the Save button based on the validation result
                btnSave.Enabled = isValid;
                btnSave.BackColor = isValid ? ColorTranslator.FromHtml("#05982E") : SystemColors.Control;
            }

            finally
            {
                isProcessingValidation = false;
            }
        }
    }
}
