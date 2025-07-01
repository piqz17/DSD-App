namespace DSD_WinformsApp.View
{
    partial class AddFormView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddFormView));
            txtCategory = new Label();
            Status_Label = new Label();
            cmbStatus = new ComboBox();
            Notes_Label = new Label();
            txtBoxNotes = new TextBox();
            cmbCategories = new ComboBox();
            label1 = new Label();
            labelCreatedBy = new Label();
            comboBoxCreatedBy = new ComboBox();
            labelDocVersion = new Label();
            textBoxDocumentVersion = new TextBox();
            labelFilename = new Label();
            labelFileUpload = new Label();
            labelDocumentNameWithExtension = new Label();
            buttonUploadDocs = new Button();
            btnSave = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // txtCategory
            // 
            txtCategory.AutoSize = true;
            txtCategory.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            txtCategory.Location = new Point(25, 214);
            txtCategory.Name = "txtCategory";
            txtCategory.Size = new Size(192, 28);
            txtCategory.TabIndex = 5;
            txtCategory.Text = "Document Category:";
            // 
            // Status_Label
            // 
            Status_Label.AutoSize = true;
            Status_Label.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            Status_Label.Location = new Point(25, 274);
            Status_Label.Name = "Status_Label";
            Status_Label.Size = new Size(165, 28);
            Status_Label.TabIndex = 7;
            Status_Label.Text = "Document Status:";
            // 
            // cmbStatus
            // 
            cmbStatus.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            cmbStatus.FormattingEnabled = true;
            cmbStatus.Location = new Point(239, 266);
            cmbStatus.Name = "cmbStatus";
            cmbStatus.Size = new Size(250, 36);
            cmbStatus.TabIndex = 8;
            // 
            // Notes_Label
            // 
            Notes_Label.AutoSize = true;
            Notes_Label.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            Notes_Label.Location = new Point(519, 149);
            Notes_Label.Name = "Notes_Label";
            Notes_Label.Size = new Size(68, 28);
            Notes_Label.TabIndex = 11;
            Notes_Label.Text = "Notes:";
            // 
            // txtBoxNotes
            // 
            txtBoxNotes.Location = new Point(519, 206);
            txtBoxNotes.MaxLength = 300;
            txtBoxNotes.Multiline = true;
            txtBoxNotes.Name = "txtBoxNotes";
            txtBoxNotes.Size = new Size(331, 156);
            txtBoxNotes.TabIndex = 12;
            // 
            // cmbCategories
            // 
            cmbCategories.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            cmbCategories.FormattingEnabled = true;
            cmbCategories.ItemHeight = 28;
            cmbCategories.Location = new Point(239, 206);
            cmbCategories.MaximumSize = new Size(250, 0);
            cmbCategories.Name = "cmbCategories";
            cmbCategories.Size = new Size(250, 36);
            cmbCategories.TabIndex = 6;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(315, 20);
            label1.Name = "label1";
            label1.Size = new Size(213, 28);
            label1.TabIndex = 10;
            label1.Text = "REGISTER DOCUMENT";
            // 
            // labelCreatedBy
            // 
            labelCreatedBy.AutoSize = true;
            labelCreatedBy.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            labelCreatedBy.Location = new Point(27, 334);
            labelCreatedBy.Name = "labelCreatedBy";
            labelCreatedBy.Size = new Size(110, 28);
            labelCreatedBy.TabIndex = 9;
            labelCreatedBy.Text = "Created By:";
            // 
            // comboBoxCreatedBy
            // 
            comboBoxCreatedBy.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            comboBoxCreatedBy.FormattingEnabled = true;
            comboBoxCreatedBy.Location = new Point(239, 326);
            comboBoxCreatedBy.Name = "comboBoxCreatedBy";
            comboBoxCreatedBy.Size = new Size(250, 36);
            comboBoxCreatedBy.TabIndex = 10;
            // 
            // labelDocVersion
            // 
            labelDocVersion.AutoSize = true;
            labelDocVersion.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            labelDocVersion.Location = new Point(25, 149);
            labelDocVersion.Name = "labelDocVersion";
            labelDocVersion.Size = new Size(184, 28);
            labelDocVersion.TabIndex = 3;
            labelDocVersion.Text = "Document Number:";
            // 
            // textBoxDocumentVersion
            // 
            textBoxDocumentVersion.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxDocumentVersion.Location = new Point(239, 141);
            textBoxDocumentVersion.Multiline = true;
            textBoxDocumentVersion.Name = "textBoxDocumentVersion";
            textBoxDocumentVersion.Size = new Size(250, 36);
            textBoxDocumentVersion.TabIndex = 4;
            // 
            // labelFilename
            // 
            labelFilename.AutoSize = true;
            labelFilename.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            labelFilename.Location = new Point(370, 85);
            labelFilename.Name = "labelFilename";
            labelFilename.Size = new Size(0, 25);
            labelFilename.TabIndex = 18;
            // 
            // labelFileUpload
            // 
            labelFileUpload.AutoSize = true;
            labelFileUpload.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            labelFileUpload.Location = new Point(25, 84);
            labelFileUpload.Name = "labelFileUpload";
            labelFileUpload.Size = new Size(177, 28);
            labelFileUpload.TabIndex = 0;
            labelFileUpload.Text = "Document Upload:";
            // 
            // labelDocumentNameWithExtension
            // 
            labelDocumentNameWithExtension.AutoSize = true;
            labelDocumentNameWithExtension.Location = new Point(452, 86);
            labelDocumentNameWithExtension.Name = "labelDocumentNameWithExtension";
            labelDocumentNameWithExtension.Size = new Size(0, 25);
            labelDocumentNameWithExtension.TabIndex = 2;
            labelDocumentNameWithExtension.Visible = false;
            // 
            // buttonUploadDocs
            // 
            buttonUploadDocs.Cursor = Cursors.Hand;
            buttonUploadDocs.FlatAppearance.BorderSize = 0;
            buttonUploadDocs.FlatAppearance.MouseOverBackColor = Color.FromArgb(9, 142, 154);
            buttonUploadDocs.FlatStyle = FlatStyle.Flat;
            buttonUploadDocs.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            buttonUploadDocs.ForeColor = Color.White;
            buttonUploadDocs.Location = new Point(239, 84);
            buttonUploadDocs.Name = "buttonUploadDocs";
            buttonUploadDocs.Size = new Size(112, 36);
            buttonUploadDocs.TabIndex = 1;
            buttonUploadDocs.Text = "Upload";
            buttonUploadDocs.UseVisualStyleBackColor = true;
            buttonUploadDocs.Click += buttonUploadDocs_Click;
            // 
            // btnSave
            // 
            btnSave.Cursor = Cursors.Hand;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatAppearance.MouseOverBackColor = Color.FromArgb(9, 142, 154);
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(600, 420);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(100, 36);
            btnSave.TabIndex = 13;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(9, 142, 154);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(750, 420);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(100, 36);
            btnCancel.TabIndex = 14;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // AddFormView
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(878, 494);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(buttonUploadDocs);
            Controls.Add(labelDocumentNameWithExtension);
            Controls.Add(textBoxDocumentVersion);
            Controls.Add(labelDocVersion);
            Controls.Add(comboBoxCreatedBy);
            Controls.Add(labelFilename);
            Controls.Add(labelCreatedBy);
            Controls.Add(labelFileUpload);
            Controls.Add(label1);
            Controls.Add(cmbCategories);
            Controls.Add(txtBoxNotes);
            Controls.Add(Notes_Label);
            Controls.Add(cmbStatus);
            Controls.Add(Status_Label);
            Controls.Add(txtCategory);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimizeBox = false;
            Name = "AddFormView";
            Text = "Add Document Form";
            Load += AddForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox Filename_Textbox;
        private Label txtCategory;
        private Label Status_Label;
        private ComboBox cmbStatus;
        private Label Notes_Label;
        private TextBox txtNotes;
        private ComboBox cmbCategories;
        private TextBox txtBoxNotes;
        private Label label1;
        private Label labelCreatedBy;
        private ComboBox comboBoxCreatedBy;
        private TextBox textBoxCreatedBy;
        private Label labelDocVersion;
        private TextBox textBoxDocumentVersion;
        private Label labelFilename;
        private Label labelFileUpload;
        private Label labelDocumentNameWithExtension;
        private Button buttonUploadDocs;
        private Button btnSave;
        private Button btnCancel;
    }
}