namespace ShopUIClassic.Adapter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Android.App;
    using Android.Content;
    using Android.OS;
    using Android.Runtime;
    using Android.Views;
    using Android.Widget;
    using ShopCommon.Models;
    using ShopUIClassic.Helpers;

    public class ProductsListAdapter : BaseAdapter<Product>
    {
        private readonly Activity context;
        private readonly List<Product> items;

        public ProductsListAdapter(Activity context, List<Product> items) : base()
        {
            this.context = context;
            this.items = items;
        }

        public override Product this[int position] =>  items[position];

        public override int Count => items.Count;

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];

            var imagenBitmap = ImageHelper.GetImageBitmapFromUrl(item.ImageFullPath);
            if (convertView == null)
            {
                convertView = context.LayoutInflater.Inflate(Resource.Layout.ProductRow, null);
            }

            convertView.FindViewById<TextView>(Resource.Id.nameTextView).Text = item.Name;
            convertView.FindViewById<TextView>(Resource.Id.priceTextView).Text = $"{item.Price:C2}";
            convertView.FindViewById<ImageView>(Resource.Id.productImageView).SetImageBitmap(imagenBitmap);

            return convertView;
        }
    }
}