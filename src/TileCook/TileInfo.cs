using System;
using System.Collections.Generic;

namespace TileCook
{
    public class TileInfo : ITileInfo
    { 
        private string _name;
        private string _description;
        private int _minzoom;
        private int _maxzoom;
        private double[] _bounds;
        private double[] _center;
        
        public TileInfo() 
        { 
            // set defaults
            _name = "";
            _description = "";
            _maxzoom = 14;
            _bounds = new double[] {-180, -90, 180, 90};
            Format = TileFormat.Pbf;
            Scheme = TileScheme.Tms;
            VectorLayers = new List<VectorLayer>();
            
        }

        public TileFormat Format { get; set; }
        public TileScheme Scheme { get; set; }
        public IEnumerable<VectorLayer> VectorLayers { get; set; }
        
        public string Name 
        { 
            get { return _name; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Name cannot be null");
                }
                if (value.Length > 255)
                {
                    throw new ArgumentException("Name cannot exceed 255 characters");
                }
                _name = value;
            }
        }
                
        public string Description
        { 
            get { return _description; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Description");
                }
                if (value.Length > 2000)
                {
                    throw new ArgumentException("Description cannot exceed 2000 characters");
                }
                _description = value;
            }
        }
        
        public int MinZoom 
        { 
            get { return _minzoom; }
            set
            {
                if (value < 0 || value > 22)
                {
                    throw new ArgumentException("MinZoom must be between 0 and 22");
                }
                _minzoom = value;
            } 
        }
        
        public int MaxZoom 
        { 
            get { return _maxzoom; }
            set
            {
                if (value < 0 || value > 22)
                {
                    throw new ArgumentException("MaxZoom must be between 0 and 22");
                }
                _maxzoom = value;
            } 
        }
        
        public double[] Bounds 
        {
            get { return _bounds; }
            set
            {
                if (value == null || value.Length != 4)
                {
                    throw new ArgumentException("Bounds must be an array in the form of [MinX,MinY,MaxX,Maxy]");
                }
                if (value[0] < -180 || value[0] > 180)
                {
                    throw new ArgumentException("Bounds MinX must be between -180 and 180");
                }
                if (value[1] < -90 || value[1] > 90)
                {
                    throw new ArgumentException("Bounds MinY must be between -90 and 90");
                }
                if (value[2] < -180 || value[2] > 180)
                {
                    throw new ArgumentException("Bounds MaxX must be between -180 and 180");
                }
                if (value[3] < -90 || value[3] > 90)
                {
                    throw new ArgumentException("Bounds MaxY must be between -90 and 90");
                }
                if (value[0] > value[2])
                {
                    throw new ArgumentException("Bounds MaxX must be greater than MinX");
                }
                if (value[1] > value[3])
                {
                    throw new ArgumentException("Bounds Maxy must be greater than Miny");
                }
                _bounds = value;
                
            }
        }
        public double[] Center
        {
            get { return _center ; }
            set
            {
                if (value != null)
                {
                    if (value.Length != 3)
                    {
                        throw new ArgumentException("Center must be an array in the form of [lon,lat,zoom]");
                    }
                    if (value[0] < -180 || value[0] > 180)
                    {
                        throw new ArgumentException("Center lon must be between -180 and 180");
                    }
                    if (value[1] < -90 || value[1] > 90)
                    {
                        throw new ArgumentException("Center lat must be between -90 and 90");
                    }
                    if (value[2] < 0 || value[2] > 22 || Math.Floor(value[2]) != value[2])
                    {
                        throw new ArgumentException("Center zoom must be a whole number between 0 and 22");
                    }
                }
                _center = value;
            }
        }
        
        public bool IsValid()
        {
            if (_minzoom > _maxzoom)
            {
                return false;
            }
            if (_center != null)
            {
                if (_center[0] < _bounds[0] || _center[0] > _bounds[2] || _center[1] < _bounds[1] || _center[1] > _bounds[3])
                {
                     return false;
                }
                if (_center[2] < _minzoom || _center[2] > _maxzoom)
                {
                     return false;
                }
            }
            if (Format == TileFormat.Pbf && VectorLayers == null)
            {
                return false;
            }
            return true;
        }
        
        public TileInfo DeepClone()
        {
            var other = new TileInfo();
            
            other.Name = Name;
            other.Description = Description;
            other.MinZoom = MinZoom;
            other.MaxZoom = MaxZoom;
            other.Bounds = (double[])Bounds.Clone();
            other.Center = Center != null ? (double[])Center.Clone() : null;
            other.Format = Format;
            other.Scheme = Scheme;
            if (VectorLayers != null)
            {
                other.VectorLayers = new List<VectorLayer>();
                foreach (var layer in VectorLayers)
                {
                    var otherLayer = new VectorLayer()
                    {
                        Id = layer.Id,
                        Description = layer.Description
                    };
                    if (layer.Fields != null)
                    {
                        otherLayer.Fields = new Dictionary<string, string>();
                        foreach (KeyValuePair<string, string> item in layer.Fields)
                        {
                            otherLayer.Fields[item.Key] = item.Value;
                        }
                    }
                }
            }
            return other;
        }
    }
}
