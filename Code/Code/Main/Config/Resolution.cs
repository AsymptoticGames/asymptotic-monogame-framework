using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AsymptoticMonoGameFramework {

    static class Resolution {

        static private GraphicsDeviceManager _Device = null;
        
        static private int _Width;
        static private int _Height;
        static private int _VWidth;
        static private int _VHeight;
        static private Matrix _ScaleMatrix;
        static private bool _FullScreen;
        static private bool _dirtyMatrix = true;
        static private int xOffset, yOffset;

        static public void Init(ref GraphicsDeviceManager device) {
            //_Width = device.PreferredBackBufferWidth;
            //_Height = device.PreferredBackBufferHeight;
            _Device = device;
            _dirtyMatrix = true;

            ResolutionConfig.SetValidResolutionOptions();
            _Width = ResolutionConfig.GetCurrentResolution().Item1;
            _Height = ResolutionConfig.GetCurrentResolution().Item2;
            _VWidth = ResolutionConfig.virtualResolution.Item1;
            _VHeight = ResolutionConfig.virtualResolution.Item2;
            _FullScreen = ResolutionConfig.isFullScreen;
            xOffset = 0;
            yOffset = 0;

            ApplyResolutionSettings();
        }

        static public Vector2 InputTranslate {
            get { return new Vector2(_Device.GraphicsDevice.Viewport.X, _Device.GraphicsDevice.Viewport.Y); }
        }

        static public Matrix InputScale {
            get { return Matrix.Invert(_ScaleMatrix); }
        }

        static public Matrix getTransformationMatrix() {
            if (_dirtyMatrix)
                RecreateScaleMatrix();

            return _ScaleMatrix;
        }

        static public void SetResolution(int Width, int Height, bool FullScreen) {
            _Width = Width;
            _Height = Height;

            if (FullScreen) { // Set Resolution to Monitor Resolution
                _Width = (int) GetMonitorResolution().X;
                _Height = (int) GetMonitorResolution().Y;
                Console.WriteLine(_Width + " " + _Height);
            }

            _FullScreen = FullScreen;

            ApplyResolutionSettings();
        }

        static public void SetVirtualResolution(int Width, int Height) {
            _VWidth = Width;
            _VHeight = Height;

            _dirtyMatrix = true;
        }

        static public Vector2 getVirtualResolution() {
            return new Vector2(_VWidth, _VHeight);
        }

        static public Vector2 GetMonitorResolution() {
            return new Vector2(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
        }

        static private void ApplyResolutionSettings() {

#if XBOX360
           _FullScreen = true;
#endif

            // If we aren't using a full screen mode, the height and width of the window can
            // be set to anything equal to or smaller than the actual screen size.
            if (_FullScreen == false) {
                if ((_Width <= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width)
                    && (_Height <= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height)) {
                    _Device.PreferredBackBufferWidth = _Width;
                    _Device.PreferredBackBufferHeight = _Height;
                    _Device.IsFullScreen = _FullScreen;
                    _Device.ApplyChanges();
                }
            } else {
                // If we are using full screen mode, we should check to make sure that the display
                // adapter can handle the video mode we are trying to set.  To do this, we will
                // iterate through the display modes supported by the adapter and check them against
                // the mode we want to set.
                foreach (DisplayMode dm in GraphicsAdapter.DefaultAdapter.SupportedDisplayModes) {
                    // Check the width and height of each mode against the passed values
                    if ((dm.Width == _Width) && (dm.Height == _Height)) {
                        // The mode is supported, so set the buffer formats, apply changes and return
                        _Device.PreferredBackBufferWidth = _Width;
                        _Device.PreferredBackBufferHeight = _Height;
                        _Device.IsFullScreen = _FullScreen;
                        _Device.ApplyChanges();
                    }
                }
            }

            _dirtyMatrix = true;

            _Width = _Device.PreferredBackBufferWidth;
            _Height = _Device.PreferredBackBufferHeight;
            _FullScreen = _Device.IsFullScreen;
        }

        /// <summary>
        /// Sets the device to use the draw pump
        /// Sets correct aspect ratio
        /// </summary>
        static public void BeginDraw() {
            // Start by reseting viewport to (0,0,1,1)
            FullViewport();
            // Clear to Black
            _Device.GraphicsDevice.Clear(Color.Black);
            // Calculate Proper Viewport according to Aspect Ratio
            ResetViewport();
            // and clear that
            // This way we are gonna have black bars if aspect ratio requires it and
            // the clear color on the rest
            _Device.GraphicsDevice.Clear(CustomColors.lightBlue);
        }

        static private void RecreateScaleMatrix() {
            _dirtyMatrix = false;
            _ScaleMatrix = Matrix.CreateScale(
                           (float)_Device.GraphicsDevice.Viewport.Width / _VWidth,
                           (float)_Device.GraphicsDevice.Viewport.Width / _VWidth,
                           1f);
        }

        static public void SetScreenShakeValues(int x, int y) {
            xOffset = x;
            yOffset = y;
        }

        static public void FullViewport() {
            Viewport vp = new Viewport();
            vp.X = vp.Y = 0;
            vp.Width = _Width;
            vp.Height = _Height;
            _Device.GraphicsDevice.Viewport = vp;
        }

        /// <summary>
        /// Get virtual Mode Aspect Ratio
        /// </summary>
        /// <returns>aspect ratio</returns>
        static public float getVirtualAspectRatio() {
            return (float)_VWidth / (float)_VHeight;
        }

        static public void ResetViewport() {
            float targetAspectRatio = getVirtualAspectRatio();
            // figure out the largest area that fits in this resolution at the desired aspect ratio
            int width = _Device.PreferredBackBufferWidth;
            int height = (int)(width / targetAspectRatio + .5f);
            bool changed = false;

            if (height > _Device.PreferredBackBufferHeight) {
                height = _Device.PreferredBackBufferHeight;
                // PillarBox
                width = (int)(height * targetAspectRatio + .5f);
                changed = true;
            }

            // set up the new viewport centered in the backbuffer
            Viewport viewport = new Viewport();

            viewport.X = (_Device.PreferredBackBufferWidth / 2) - (width / 2) + xOffset;
            viewport.Y = (_Device.PreferredBackBufferHeight / 2) - (height / 2) + yOffset;
            viewport.Width = width;
            viewport.Height = height;
            viewport.MinDepth = 0;
            viewport.MaxDepth = 1;

            if (changed) {
                _dirtyMatrix = true;
            }

            _Device.GraphicsDevice.Viewport = viewport;
        }

    }
}
