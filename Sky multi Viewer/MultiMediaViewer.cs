using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using Sky_multi_Core.ImageReader;

namespace Sky_multi_Viewer
{
    public delegate void EventMediaTypedHandler(bool DifferentBackMedia);

    public class MultiMediaViewer: VideoView
    {
        private ImageView imageView = new ImageView();

        public EventMediaTypedHandler ItIsPicture = null;
        public EventMediaTypedHandler ItIsAudioOrVideo = null;

        public bool ItIsAImage { get; private set; } = false;
        public bool RawImage { get; private set; } = false;

        public MultiMediaViewer(): base()
        {
            imageView.Location = new Point(0, 0);
            imageView.Size = this.Size;
            imageView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            imageView.Visible = false;
            this.Controls.Add(imageView);

            imageView.MouseClick += new MouseEventHandler(MouseClickEvent);
            imageView.MouseDoubleClick += new MouseEventHandler(MouseDoubleClickEvent);
            imageView.MouseDown += new MouseEventHandler(MouseDownEvent);
            imageView.MouseUp += new MouseEventHandler(MouseUpEvent);
            imageView.MouseMove += new MouseEventHandler(MouseMoveEvent);
            imageView.MouseLeave += new EventHandler(MouseLeaveEvent);
            imageView.MouseEnter += new EventHandler(MouseEnterEvent);
            imageView.MouseWheel += new MouseEventHandler(MouseWheelEvent);
            imageView.MouseHover += new EventHandler(MouseHoverEvent);
        }

        private void MouseClickEvent(object sender, MouseEventArgs e)
        {
            OnMouseClick(e);
        }
        private void MouseDoubleClickEvent(object sender, MouseEventArgs e)
        {
            OnMouseDoubleClick(e);
        }
        private void MouseDownEvent(object sender, MouseEventArgs e)
        {
            OnMouseDown(e);
        }
        private void MouseUpEvent(object sender, MouseEventArgs e)
        {
            OnMouseUp(e);
        }
        private void MouseMoveEvent(object sender, MouseEventArgs e)
        {
            OnMouseMove(e);
        }
        private void MouseLeaveEvent(object sender, EventArgs e)
        {
            OnMouseLeave(e);
        }
        private void MouseEnterEvent(object sender, EventArgs e)
        {
            OnMouseEnter(e);
        }

        private void MouseWheelEvent(object sender, MouseEventArgs e)
        {
            OnMouseWheel(e);
        }

        private void MouseHoverEvent(object sender, EventArgs e)
        {
            OnMouseHover(e);
        }

        public void OpenCD(string CDPath)
        {
            imageView.Visible = false;

            if (imageView.Image != null)
            {
                imageView.RemoveImage();
            }

            if (ItIsAudioOrVideo != null)
            {
                ItIsAudioOrVideo(ItIsAImage);
            }

            ItIsAImage = false;
            this.SetMedia("dvd:///" + CDPath);
            this.Play();
        }

        public void OpenDirectory(string DirectoryPath)
        {

        }

        public void OpenFile(string FilePath, params string[] options)
        {
            try
            {
                imageView.DecodeImageFile(FilePath);
            }
            catch
            {
                imageView.Visible = false;

                if (imageView.Image != null)
                {
                    imageView.RemoveImage();
                }

                if (ItIsAudioOrVideo != null)
                {
                    ItIsAudioOrVideo(ItIsAImage);
                }

                ItIsAImage = false;
                this.SetMedia("File:///" + FilePath, options);
                this.Play();
                return;
            }

            imageView.Visible = true;
            this.Stop();

            if (ItIsPicture != null)
            {
                ItIsPicture(!ItIsAImage);
            }

            ItIsAImage = true;
        }

        public void RotateImage()
        {
            imageView.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            imageView.Refresh();
        }
    }
}
