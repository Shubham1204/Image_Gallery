using C1.C1Pdf;
using C1.Win.C1Tile;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageGallery
{
    public partial class ImageGallery : Form
    {

        DataFetcher datafetch = new DataFetcher();
        Panel panel1 = new Panel();
        Panel panel2 = new Panel();
        TextBox text_box = new TextBox();
        PictureBox picture_box_search = new PictureBox();
        List<ImageItem> imagesList;
        int checkedItems = 0;
        PictureBox picture_box_export = new PictureBox();
        Label label_export = new Label();
        PictureBox picture_box_save = new PictureBox();
        Label label_save = new Label();
        Group group1 = new Group();
        Tile tile1 = new Tile();
        Tile tile2 = new Tile();
        Tile tile3 = new Tile();
        PanelElement panel_element = new PanelElement();
        ImageElement image_element = new ImageElement();
        TextElement text_element = new TextElement();
        StatusStrip strip = new StatusStrip();
        TableLayoutPanel table_layout_panel = new TableLayoutPanel();
        SplitContainer split_container = new SplitContainer();
        C1TileControl tile = new C1TileControl();
        C1PdfDocument imagePdfDocument = new C1PdfDocument();
        ToolStripProgressBar pbar = new ToolStripProgressBar();


        //Values are initialized in the constructor
        public ImageGallery()
        {
            InitializeComponent();

            //Form Properties
            this.Text = "ImageGallery";
            this.StartPosition = FormStartPosition.CenterParent;
            this.ShowIcon = false;
            this.MaximizeBox = false;
            this.Size = new Size(780, 800);
            this.MaximumSize = new Size(810, 810);
            this.ShowIcon = false;


            //SearchBox
            text_box.Name = "_searchBox";
            text_box.Text = "Search Image";
            text_box.BorderStyle = BorderStyle.None;
            text_box.Location = new System.Drawing.Point(16, 9);
            text_box.Dock = DockStyle.Fill;
            text_box.Size = new Size(244, 16);
            text_box.Anchor = (AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom);


            //PictureBox
            picture_box_search.Name = "_search";
            picture_box_search.Dock = DockStyle.Left;
            picture_box_search.Location = new System.Drawing.Point(0, 0);
            picture_box_search.Margin = new Padding(0, 0, 0, 0);
            picture_box_search.Size = new Size(40, 16);
            picture_box_search.SizeMode = PictureBoxSizeMode.Zoom;
            picture_box_search.ImageLocation = "../../Images/search.png";
            picture_box_search.Anchor = (AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom);
            picture_box_search.Click += new EventHandler(this._search_Click);


            //export button
            picture_box_export.Name = "_exportImage";
            picture_box_export.Location = new System.Drawing.Point(29, 1);
            picture_box_export.Size = new Size(75, 25);
            picture_box_export.SizeMode = PictureBoxSizeMode.StretchImage;
            picture_box_export.ImageLocation = "https://cdn4.iconfinder.com/data/icons/flat-design-word-processing-set-3/24/export-pdf-512.png";
            picture_box_export.Visible = false;
            picture_box_export.Click += new EventHandler(this._exportImage_Click);
            picture_box_export.Paint += new PaintEventHandler(this._exportImage_Paint);


            //Export to Pdf label
            label_export.Name = "_exportLabel";
            label_export.Text = "Export To Pdf";
            label_export.Location = new System.Drawing.Point(29, 28);
            label_export.Size = new Size(75, 15);
            label_export.Visible = false;


            //save to disk button
            picture_box_save.Name = "_saveImage";
            picture_box_save.Location = new System.Drawing.Point(120, 1);
            picture_box_save.Size = new Size(85, 28);
            picture_box_save.SizeMode = PictureBoxSizeMode.StretchImage;
            picture_box_save.ImageLocation = "https://freesvg.org/img/document-save-as.png";
            picture_box_save.Visible = false;
            picture_box_save.Click += new EventHandler(this._saveImage_Click);
            picture_box_save.Paint += new PaintEventHandler(this._saveImage_Paint);


            //Save To Disk Label
            label_save.Name = "_saveLabel";
            label_save.Text = "Save To Disk";
            label_save.Location = new System.Drawing.Point(125, 28);
            label_save.Size = new Size(75, 15);
            label_save.Visible = false;


            // Table Layout Panel
            table_layout_panel.ColumnCount = 3;
            table_layout_panel.Location = new System.Drawing.Point(0, 0);
            table_layout_panel.Dock = DockStyle.Fill;
            table_layout_panel.RowCount = 1;
            table_layout_panel.Visible = true;
            table_layout_panel.Size = new Size(800, 40);
            table_layout_panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            table_layout_panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 37.50F));
            table_layout_panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 37.50F));
            table_layout_panel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            table_layout_panel.Controls.Add(panel1, 1, 0);
            table_layout_panel.Controls.Add(panel2, 2, 0);


            // Panels
            panel1.Size = new Size(287, 40);
            panel1.Location = new System.Drawing.Point(477, 0);
            panel1.Dock = DockStyle.Fill;
            panel1.Controls.Add(text_box);
            panel1.Paint += new PaintEventHandler(this.panel1_Paint);
            panel2.Size = new Size(40, 16);
            panel2.Location = new System.Drawing.Point(479, 12);
            panel2.TabIndex = 1;
            panel2.Margin = new Padding(2, 12, 45, 12);
            panel2.Controls.Add(picture_box_search);


            // Tiles
            // tile1
            tile1.BackColor = System.Drawing.Color.LightCoral;
            tile1.Name = "tile1";
            tile1.Text = "Tile 1";

            // tile2 
            tile2.BackColor = System.Drawing.Color.Teal;
            tile2.Name = "tile2";
            tile2.Text = "Tile 2";

            // tile3
            tile3.BackColor = System.Drawing.Color.SteelBlue;
            tile3.Name = "tile3";
            tile3.Text = "Tile 3";



            // Group
            group1.Name = "group1";
            group1.Text = "Search Results";
            group1.Tiles.Add(this.tile1);
            group1.Tiles.Add(this.tile2);
            group1.Tiles.Add(this.tile3);
            group1.Visible = false;



            // PanelElement
            panel_element.Alignment = ContentAlignment.BottomLeft;
            panel_element.Margin = new Padding(10, 6, 10, 6);
            panel_element.Children.Add(image_element);
            panel_element.Children.Add(text_element);


            // TileControl
            tile.Name = "_imageTileControl";
            tile.AllowChecking = true;
            tile.AllowRearranging = true;
            tile.CellHeight = 78;
            tile.CellSpacing = 11;
            tile.CellWidth = 78;
            tile.Dock = DockStyle.Fill;
            tile.Size = new Size(764, 718);
            tile.SurfacePadding = new Padding(12, 4, 12, 4);
            tile.SwipeDistance = 20;
            tile.SwipeRearrangeDistance = 98;
            tile.Groups.Add(group1);
            tile.DefaultTemplate.Elements.Add(panel_element);
            tile.Location = new Point(0, 0);
            tile.TileChecked += new System.EventHandler<C1.Win.C1Tile.TileEventArgs>(this._ImageTileControl_TileChecked);
            tile.TileUnchecked += new System.EventHandler<C1.Win.C1Tile.TileEventArgs>(this._ImageTileControl_TileUnchecked);
            tile.Paint += new System.Windows.Forms.PaintEventHandler(this._ImageTileControl_Paint);


            //Strip
            strip.Dock = DockStyle.Bottom;
            strip.Visible = false;
            pbar.Style = ProgressBarStyle.Marquee;
            strip.Items.Add(pbar);


            // Split Controller
            split_container.Dock = DockStyle.Fill;
            split_container.Margin = new Padding(2);
            split_container.Orientation = Orientation.Horizontal;
            split_container.SplitterDistance = 50;
            split_container.FixedPanel = FixedPanel.Panel1;
            split_container.IsSplitterFixed = true;
            split_container.Panel1.Controls.Add(table_layout_panel);
            split_container.Panel2.Controls.Add(picture_box_export);
            split_container.Panel2.Controls.Add(label_export);
            split_container.Panel2.Controls.Add(label_save);
            split_container.Panel2.Controls.Add(picture_box_save);
            split_container.Panel2.Controls.Add(tile);
            split_container.Panel2.Controls.Add(strip);
            this.Controls.Add(split_container);

        }

        //Implemented Search functionality
        private async void _search_Click(object sender, EventArgs e)
        {
            strip.Visible = true;
            // fetches images from DataFetcher.cs
            imagesList = await datafetch.GetImageData(text_box.Text);
            AddTiles(imagesList);
            strip.Visible = false;
        }


        //Adds Tiles dynamically to the panel
        private void AddTiles(List<ImageItem> imageList)
        {
            tile.Groups[0].Tiles.Clear();
            group1.Visible = true;
            foreach (var imageitem in imageList)
            {
                Tile inner_tile = new Tile();
                inner_tile.HorizontalSize = 2;
                inner_tile.VerticalSize = 2;
                tile.Groups[0].Tiles.Add(inner_tile);
                Image img = Image.FromStream(new MemoryStream(imageitem.Base64));
                Template template = new Template();
                ImageElement image_element = new ImageElement();
                image_element.ImageLayout = ForeImageLayout.Stretch;
                template.Elements.Add(image_element);
                inner_tile.Template = template;
                inner_tile.Image = img;
            }
            
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Rectangle r = text_box.Bounds;
            r.Inflate(3, 3);
            Pen p = new Pen(Color.LightGray);
            e.Graphics.DrawRectangle(p, r);
        }


        //Exports the images to Pdf
        private void _exportImage_Click(object sender, EventArgs e)
        {
            List<Image> images = new List<Image>();
            foreach (Tile inner_tile in tile.Groups[0].Tiles)
            {
                if (inner_tile.Checked)
                {
                    images.Add(inner_tile.Image);
                }
            }
            ConvertToPdf(images);
            SaveFileDialog saveFile = new SaveFileDialog
            {
                DefaultExt = "pdf",
                Filter = "PDF files (*.pdf)|*.pdf*"
            };

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                imagePdfDocument.Save(saveFile.FileName);
            }
        }

        
        private void ConvertToPdf(List<Image> images)
        {
            RectangleF rect = imagePdfDocument.PageRectangle;
            bool firstPage = true;
            foreach (var selectedimg in images)
            {
                if (!firstPage)
                {
                    imagePdfDocument.NewPage();
                }
                firstPage = false;
                rect.Inflate(-72, -72);
                imagePdfDocument.DrawImage(selectedimg, rect);
            }
        }

        private void _exportImage_Paint(object sender, PaintEventArgs e)
        {
            Rectangle r = new Rectangle(picture_box_export.Location.X,
            picture_box_export.Location.Y, picture_box_export.Width, picture_box_export.Height);
            r.X -= 29;
            r.Y -= 3;
            r.Width--;
            r.Height--;
            Pen p = new Pen(Color.LightGray);
            e.Graphics.DrawRectangle(p, r);
            e.Graphics.DrawLine(p, new Point(0, 43), new
           Point(this.Width, 43));
        }


        // Saves the images to the Disk
        private void _saveImage_Click(Object sender, EventArgs e)
        {
            List<Image> images1 = new List<Image>();
            foreach (Tile inner_tile1 in tile.Groups[0].Tiles)
            {
                if (inner_tile1.Checked)
                {
                    images1.Add(inner_tile1.Image);
                }
            }
            SaveFileDialog saveFile = new SaveFileDialog
            {
                DefaultExt = "jpg",
                Filter = "JPG files (*.jpg)|*.jpg*"
            };
            foreach (Image img in images1)
            {
                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    img.Save(saveFile.FileName);
                }
            }
        }

        private void _saveImage_Paint(object sender, PaintEventArgs e)
        {
            Rectangle r = new Rectangle(picture_box_save.Location.X,
            picture_box_save.Location.Y, picture_box_save.Width, picture_box_save.Height);
            r.X -= 29;
            r.Y -= 3;
            r.Width--;
            r.Height--;
            Pen p = new Pen(Color.LightGray);
            e.Graphics.DrawRectangle(p, r);
            e.Graphics.DrawLine(p, new Point(0, 43), new
           Point(this.Width, 43));
        }


        //Selects an image to perform an action
        private void _ImageTileControl_TileChecked(object sender, TileEventArgs e)
        {
            checkedItems++;
            picture_box_export.Visible = true;
            picture_box_save.Visible = true;
            label_export.Visible = true;
            label_save.Visible = true;
        }

        private void _ImageTileControl_TileUnchecked(object sender, TileEventArgs e)
        {
            checkedItems--;
            picture_box_export.Visible = checkedItems > 0;
            picture_box_save.Visible = checkedItems > 0;
            label_export.Visible = checkedItems > 0;
            label_save.Visible = checkedItems > 0;
        }

        private void _ImageTileControl_Paint(object sender, PaintEventArgs e)
        {
            Pen p = new Pen(Color.LightGray);
            e.Graphics.DrawLine(p, 0, 43, 800, 43);
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
