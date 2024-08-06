// Cyotek Color Picker Controls Library
// http://cyotek.com/blog/tag/colorpicker

// Copyright (c) 2013-2021 Cyotek Ltd.

// This work is licensed under the MIT License.
// See LICENSE.TXT for the full text

// Found this code useful?
// https://www.cyotek.com/contribute

using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace Deniz.TiberiumSunEditor.Gui.Controls.ColorControls
{
  /// <summary>
  /// Represents a control for selecting a value from a scale
  /// </summary>
  [DefaultValue("Value")]
  [DefaultEvent("ValueChanged")]
  public class ColorSlider : UserControl
  {
      private static readonly object _eventBarBoundsChanged = new object();

    private static readonly object _eventBarPaddingChanged = new object();

    private static readonly object _eventBarStyleChanged = new object();

    private static readonly object _eventColor1Changed = new object();

    private static readonly object _eventColor2Changed = new object();

    private static readonly object _eventColor3Changed = new object();

    private static readonly object _eventCustomColorsChanged = new object();

    private static readonly object _eventDividerStyleChanged = new object();

    private static readonly object _eventLargeChangeChanged = new object();

    private static readonly object _eventMaximumChanged = new object();

    private static readonly object _eventMinimumChanged = new object();

    private static readonly object _eventNubColorChanged = new object();

    private static readonly object _eventNubOutlineColorChanged = new object();

    private static readonly object _eventNubSizeChanged = new object();

    private static readonly object _eventNubStyleChanged = new object();

    private static readonly object _eventOrientationChanged = new object();

    private static readonly object _eventShowValueDividerChanged = new object();

    private static readonly object _eventSmallChangeChanged = new object();

    private static readonly object _eventValueChanged = new object();

    private static readonly float[] _pairPositions = { 0F, 1F };

    private static readonly float[] _triplePositions = { 0F, 0.5F, 1F };

    private Rectangle _barBounds;

    private Padding _barPadding;

    private ColorBarStyle _barStyle;

    private System.Drawing.Color _color1;

    private System.Drawing.Color _color2;

    private System.Drawing.Color _color3;

    private ColorCollection _customColors;

    private int _largeChange;

    private float _maximum;

    private float _minimum;

    private System.Drawing.Color _nubColor;

    private System.Drawing.Color _nubOutlineColor;

    private Size _nubSize;

    private ColorSliderNubStyle _nubStyle;

    private Orientation _orientation;

    private Image? _selectionGlyph;

    private bool _showValueDivider;

    private int _smallChange;

    private float _value;

    /// <summary>
    /// Initializes a new instance of the <see cref="ColorSlider"/> class.
    /// </summary>
    public ColorSlider()
    {
      this.SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.Selectable, true);
      _orientation = Orientation.Horizontal;
      _color1 = System.Drawing.Color.Black;
      _color2 = System.Drawing.Color.FromArgb(127, 127, 127);
      _color3 = System.Drawing.Color.White;
      _minimum = 0;
      _maximum = 100;
      _nubStyle = ColorSliderNubStyle.BottomRight;
      _nubSize = new Size(8, 8);
      _nubColor = System.Drawing.Color.Black;
      _nubOutlineColor = System.Drawing.Color.White;
      _smallChange = 1;
      _largeChange = 10;
    }

    [Category("Property Changed")]
    public event EventHandler BarBoundsChanged
    {
      add => this.Events.AddHandler(_eventBarBoundsChanged, value);
      remove => this.Events.RemoveHandler(_eventBarBoundsChanged, value);
    }

    [Category("Property Changed")]
    public event EventHandler BarPaddingChanged
    {
      add => this.Events.AddHandler(_eventBarPaddingChanged, value);
      remove => this.Events.RemoveHandler(_eventBarPaddingChanged, value);
    }

    [Category("Property Changed")]
    public event EventHandler BarStyleChanged
    {
      add => this.Events.AddHandler(_eventBarStyleChanged, value);
      remove => this.Events.RemoveHandler(_eventBarStyleChanged, value);
    }

    [Category("Property Changed")]
    public event EventHandler Color1Changed
    {
      add => this.Events.AddHandler(_eventColor1Changed, value);
      remove => this.Events.RemoveHandler(_eventColor1Changed, value);
    }

    [Category("Property Changed")]
    public event EventHandler Color2Changed
    {
      add => this.Events.AddHandler(_eventColor2Changed, value);
      remove => this.Events.RemoveHandler(_eventColor2Changed, value);
    }

    [Category("Property Changed")]
    public event EventHandler Color3Changed
    {
      add => this.Events.AddHandler(_eventColor3Changed, value);
      remove => this.Events.RemoveHandler(_eventColor3Changed, value);
    }

    [Category("Property Changed")]
    public event EventHandler CustomColorsChanged
    {
      add => this.Events.AddHandler(_eventCustomColorsChanged, value);
      remove => this.Events.RemoveHandler(_eventCustomColorsChanged, value);
    }

    [Category("Property Changed")]
    public event EventHandler DividerStyleChanged
    {
      add => this.Events.AddHandler(_eventDividerStyleChanged, value);
      remove => this.Events.RemoveHandler(_eventDividerStyleChanged, value);
    }

    [Category("Property Changed")]
    public event EventHandler LargeChangeChanged
    {
      add => this.Events.AddHandler(_eventLargeChangeChanged, value);
      remove => this.Events.RemoveHandler(_eventLargeChangeChanged, value);
    }

    [Category("Property Changed")]
    public event EventHandler MaximumChanged
    {
      add => this.Events.AddHandler(_eventMaximumChanged, value);
      remove => this.Events.RemoveHandler(_eventMaximumChanged, value);
    }

    [Category("Property Changed")]
    public event EventHandler MinimumChanged
    {
      add => this.Events.AddHandler(_eventMinimumChanged, value);
      remove => this.Events.RemoveHandler(_eventMinimumChanged, value);
    }

    [Category("Property Changed")]
    public event EventHandler NubColorChanged
    {
      add => this.Events.AddHandler(_eventNubColorChanged, value);
      remove => this.Events.RemoveHandler(_eventNubColorChanged, value);
    }

    /// <summary>
    /// Occurs when the NubOutlineColor property value changes
    /// </summary>
    [Category("Property Changed")]
    public event EventHandler NubOutlineColorChanged
    {
      add
      {
        this.Events.AddHandler(_eventNubOutlineColorChanged, value);
      }
      remove
      {
        this.Events.RemoveHandler(_eventNubOutlineColorChanged, value);
      }
    }

    [Category("Property Changed")]
    public event EventHandler NubSizeChanged
    {
      add => this.Events.AddHandler(_eventNubSizeChanged, value);
      remove => this.Events.RemoveHandler(_eventNubSizeChanged, value);
    }

    [Category("Property Changed")]
    public event EventHandler NubStyleChanged
    {
      add => this.Events.AddHandler(_eventNubStyleChanged, value);
      remove => this.Events.RemoveHandler(_eventNubStyleChanged, value);
    }

    [Category("Property Changed")]
    public event EventHandler OrientationChanged
    {
      add => this.Events.AddHandler(_eventOrientationChanged, value);
      remove => this.Events.RemoveHandler(_eventOrientationChanged, value);
    }

    [Category("Property Changed")]
    public event EventHandler ShowValueDividerChanged
    {
      add => this.Events.AddHandler(_eventShowValueDividerChanged, value);
      remove => this.Events.RemoveHandler(_eventShowValueDividerChanged, value);
    }

    [Category("Property Changed")]
    public event EventHandler SmallChangeChanged
    {
      add => this.Events.AddHandler(_eventSmallChangeChanged, value);
      remove => this.Events.RemoveHandler(_eventSmallChangeChanged, value);
    }

    [Category("Property Changed")]
    public event EventHandler ValueChanged
    {
      add => this.Events.AddHandler(_eventValueChanged, value);
      remove => this.Events.RemoveHandler(_eventValueChanged, value);
    }

    /// <summary>
    /// Gets or sets the location and size of the color bar.
    /// </summary>
    /// <value>The location and size of the color bar.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual Rectangle BarBounds
    {
      get => _barBounds;
      protected set
      {
        if (_barBounds != value)
        {
          _barBounds = value;

          this.OnBarBoundsChanged(EventArgs.Empty);
        }
      }
    }

    /// <summary>
    /// Gets or sets the bar padding.
    /// </summary>
    /// <value>The bar padding.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual Padding BarPadding
    {
      get => _barPadding;
      protected set
      {
        if (_barPadding != value)
        {
          _barPadding = value;

          this.OnBarPaddingChanged(EventArgs.Empty);
        }
      }
    }

    /// <summary>
    /// Gets or sets the bar style.
    /// </summary>
    /// <value>The bar style.</value>
    [Category("Appearance")]
    [DefaultValue(typeof(ColorBarStyle), "TwoColor")]
    public virtual ColorBarStyle BarStyle
    {
      get => _barStyle;
      set
      {
        if (_barStyle != value)
        {
          _barStyle = value;

          this.OnBarStyleChanged(EventArgs.Empty);
        }
      }
    }

    /// <summary>
    /// Gets or sets the first color of the bar.
    /// </summary>
    /// <value>The first color.</value>
    /// <remarks>This property is ignored if the <see cref="BarStyle"/> property is set to Custom and a valid color set has been specified</remarks>
    [Category("Appearance")]
    [DefaultValue(typeof(System.Drawing.Color), "Black")]
    public virtual System.Drawing.Color Color1
    {
      get => _color1;
      set
      {
        if (_color1 != value)
        {
          _color1 = value;

          this.OnColor1Changed(EventArgs.Empty);
        }
      }
    }

    /// <summary>
    /// Gets or sets the second color of the bar.
    /// </summary>
    /// <value>The second color.</value>
    /// <remarks>This property is ignored if the <see cref="BarStyle"/> property is set to Custom and a valid color set has been specified</remarks>
    [Category("Appearance")]
    [DefaultValue(typeof(System.Drawing.Color), "127, 127, 127")]
    public virtual System.Drawing.Color Color2
    {
      get => _color2;
      set
      {
        if (_color2 != value)
        {
          _color2 = value;

          this.OnColor2Changed(EventArgs.Empty);
        }
      }
    }

    /// <summary>
    /// Gets or sets the third color of the bar.
    /// </summary>
    /// <value>The third color.</value>
    /// <remarks>This property is ignored if the <see cref="BarStyle"/> property is set to Custom and a valid color set has been specified, or if the BarStyle is set to TwoColor.</remarks>
    [Category("Appearance")]
    [DefaultValue(typeof(System.Drawing.Color), "White")]
    public virtual System.Drawing.Color Color3
    {
      get => _color3;
      set
      {
        if (_color3 != value)
        {
          _color3 = value;

          this.OnColor3Changed(EventArgs.Empty);
        }
      }
    }

    /// <summary>
    /// Gets or sets the color range used by the custom bar style.
    /// </summary>
    /// <value>The custom colors.</value>
    /// <remarks>This property is ignored if the <see cref="BarStyle"/> property is not set to Custom</remarks>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual ColorCollection CustomColors
    {
      get => _customColors;
      set
      {
        if (!object.ReferenceEquals(_customColors, value))
        {
          _customColors = value;

          this.OnCustomColorsChanged(EventArgs.Empty);
        }
      }
    }

    /// <summary>
    /// Gets or sets the font of the text displayed by the control.
    /// </summary>
    /// <value>The font.</value>
    /// <returns>The <see cref="T:System.Drawing.Font" /> to apply to the text displayed by the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultFont" /> property.</returns>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Font Font
    {
      get => base.Font;
      set => base.Font = value;
    }

    /// <summary>
    /// Gets or sets the foreground color of the control.
    /// </summary>
    /// <value>The color of the fore.</value>
    /// <returns>The foreground <see cref="T:System.Drawing.Color" /> of the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultForeColor" /> property.</returns>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override System.Drawing.Color ForeColor
    {
      get => base.ForeColor;
      set => base.ForeColor = value;
    }

    /// <summary>
    /// Gets or sets a value to be added to or subtracted from the <see cref="Value"/> property when the selection is moved a large distance.
    /// </summary>
    /// <value>A numeric value. The default value is 10.</value>
    [Category("Behavior")]
    [DefaultValue(10)]
    public virtual int LargeChange
    {
      get => _largeChange;
      set
      {
        if (_largeChange != value)
        {
          _largeChange = value;

          this.OnLargeChangeChanged(EventArgs.Empty);
        }
      }
    }

    /// <summary>
    /// Gets or sets the upper limit of values of the selection range.
    /// </summary>
    /// <value>A numeric value. The default value is 100.</value>
    [Category("Behavior")]
    [DefaultValue(100F)]
    public virtual float Maximum
    {
      get => _maximum;
      set
      {
        if (Math.Abs(_maximum - value) > float.Epsilon)
        {
          _maximum = value;

          this.OnMaximumChanged(EventArgs.Empty);
        }
      }
    }

    /// <summary>
    /// Gets or sets the lower limit of values of the selection range.
    /// </summary>
    /// <value>A numeric value. The default value is 0.</value>
    [Category("Behavior")]
    [DefaultValue(0F)]
    public virtual float Minimum
    {
      get => _minimum;
      set
      {
        if (Math.Abs(_minimum - value) > float.Epsilon)
        {
          _minimum = value;

          this.OnMinimumChanged(EventArgs.Empty);
        }
      }
    }

    /// <summary>
    /// Gets or sets the color of the selection nub.
    /// </summary>
    /// <value>The color of the nub.</value>
    [Category("Appearance")]
    [DefaultValue(typeof(System.Drawing.Color), "Black")]
    public virtual System.Drawing.Color NubColor
    {
      get => _nubColor;
      set
      {
        if (_nubColor != value)
        {
          _nubColor = value;

          this.OnNubColorChanged(EventArgs.Empty);
        }
      }
    }

    [Category("Appearance")]
    [DefaultValue(typeof(System.Drawing.Color), "White")]
    public System.Drawing.Color NubOutlineColor
    {
      get { return _nubOutlineColor; }
      set
      {
        if (_nubOutlineColor != value)
        {
          _nubOutlineColor = value;

          this.OnNubOutlineColorChanged(EventArgs.Empty);
        }
      }
    }

    /// <summary>
    /// Gets or sets the size of the selection nub.
    /// </summary>
    /// <value>The size of the nub.</value>
    [Category("Appearance")]
    [DefaultValue(typeof(Size), "8, 8")]
    public virtual Size NubSize
    {
      get => _nubSize;
      set
      {
        if (_nubSize != value)
        {
          _nubSize = value;

          this.OnNubSizeChanged(EventArgs.Empty);
        }
      }
    }

    /// <summary>
    /// Gets or sets the selection nub style.
    /// </summary>
    /// <value>The nub style.</value>
    [Category("Appearance")]
    [DefaultValue(typeof(ColorSliderNubStyle), "BottomRight")]
    public virtual ColorSliderNubStyle NubStyle
    {
      get => _nubStyle;
      set
      {
        if (_nubStyle != value)
        {
          _nubStyle = value;

          this.OnNubStyleChanged(EventArgs.Empty);
        }
      }
    }

    /// <summary>
    /// Gets or sets the orientation of the color bar.
    /// </summary>
    /// <value>The orientation.</value>
    [Category("Appearance")]
    [DefaultValue(typeof(Orientation), "Horizontal")]
    public virtual Orientation Orientation
    {
      get => _orientation;
      set
      {
        if (_orientation != value)
        {
          _orientation = value;

          this.OnOrientationChanged(EventArgs.Empty);
        }
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether a divider is shown at the selection nub location.
    /// </summary>
    /// <value><c>true</c> if a value divider is to be shown; otherwise, <c>false</c>.</value>
    [Category("Appearance")]
    [DefaultValue(false)]
    public virtual bool ShowValueDivider
    {
      get => _showValueDivider;
      set
      {
        if (_showValueDivider != value)
        {
          _showValueDivider = value;

          this.OnShowValueDividerChanged(EventArgs.Empty);
        }
      }
    }

    /// <summary>
    /// Gets or sets the value to be added to or subtracted from the <see cref="Value"/> property when the selection is moved a small distance.
    /// </summary>
    /// <value>A numeric value. The default value is 1.</value>
    [Category("Behavior")]
    [DefaultValue(1)]
    public virtual int SmallChange
    {
      get => _smallChange;
      set
      {
        if (_smallChange != value)
        {
          _smallChange = value;

          this.OnSmallChangeChanged(EventArgs.Empty);
        }
      }
    }

    /// <summary>
    /// Gets or sets the text associated with this control.
    /// </summary>
    /// <value>The text.</value>
    /// <returns>The text associated with this control.</returns>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override string Text
    {
      get => base.Text;
      set => base.Text = value;
    }

    /// <summary>
    /// Gets or sets a numeric value that represents the current position of the selection numb on the color slider control.
    /// </summary>
    /// <value>A numeric value that is within the <see cref="Minimum"/> and <see cref="Maximum"/> range. The default value is 0.</value>
    [Category("Appearance")]
    [DefaultValue(0F)]
    public virtual float Value
    {
      get => _value;
      set
      {
        value = this.ClampValue(value);

        if (Math.Abs(_value - value) > float.Epsilon)
        {
          _value = value;

          this.OnValueChanged(EventArgs.Empty);
        }
      }
    }

    /// <summary>
    /// Gets or sets the selection glyph.
    /// </summary>
    /// <value>The selection glyph.</value>
    protected Image? SelectionGlyph
    {
      get => _selectionGlyph;
      set => _selectionGlyph = value;
    }

    /// <summary>
    /// Creates the selection nub glyph.
    /// </summary>
    /// <returns>Image.</returns>
    protected virtual Image? CreateNubGlyph()
    {
      return null;
    }

    /// <summary>
    /// Defines the bar bounds and padding.
    /// </summary>
    protected virtual void DefineBar()
    {
      // TODO: Property is protected so in theory a custom value could have been set
      _selectionGlyph?.Dispose();

      _barPadding = this.GetBarPadding();
      _barBounds = this.GetBarBounds();

      _selectionGlyph = _nubStyle == ColorSliderNubStyle.None
        ? null
        : this.CreateNubGlyph();
    }

    /// <summary>
    /// Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.Control" /> and its child controls and optionally releases the managed resources.
    /// </summary>
    /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && _selectionGlyph != null)
      {
        _selectionGlyph.Dispose();
      }

      base.Dispose(disposing);
    }

    /// <summary>
    /// Gets the bar bounds.
    /// </summary>
    /// <returns>Rectangle.</returns>
    protected virtual Rectangle GetBarBounds()
    {
        var size = this.ClientSize;
      var padding = _barPadding + this.Padding;

      return new Rectangle(padding.Left, padding.Top, size.Width - padding.Horizontal, size.Height - padding.Vertical);
    }

    /// <summary>
    /// Gets the bar padding.
    /// </summary>
    /// <returns>Padding.</returns>
    protected virtual Padding GetBarPadding()
    {
        var left = 0;
      var top = 0;
      var right = 0;
      var bottom = 0;

      if (_nubStyle != ColorSliderNubStyle.None)
      {
          var hw = _nubSize.Width / 2 + 1;
        var hh = _nubSize.Height / 2 + 1;

        if (_orientation == Orientation.Horizontal)
        {
          left = hw;
          right = hw;

          if (_nubStyle == ColorSliderNubStyle.BottomRight)
          {
            bottom = hh;
          }
          else
          {
            top = hh;
          }
        }
        else
        {
          top = hh;
          bottom = hh;

          if (_nubStyle == ColorSliderNubStyle.BottomRight)
          {
            right = hw;
          }
          else
          {
            left = hw;
          }
        }
      }

      return new Padding(left, top, right, bottom);
    }

    /// <summary>
    /// Determines whether the specified key is a regular input key or a special key that requires preprocessing.
    /// </summary>
    /// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values.</param>
    /// <returns>true if the specified key is a regular input key; otherwise, false.</returns>
    protected override bool IsInputKey(Keys keyData)
    {
      return ColorSlider.IsNavigationKey(keyData) || base.IsInputKey(keyData);
    }

    /// <summary>
    /// Raises the <see cref="BarBoundsChanged" /> event.
    /// </summary>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    protected virtual void OnBarBoundsChanged(EventArgs e)
    {
        var handler = this.Events[_eventBarBoundsChanged] as EventHandler;

      handler?.Invoke(this, e);
    }

    /// <summary>
    /// Raises the <see cref="BarPaddingChanged" /> event.
    /// </summary>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    protected virtual void OnBarPaddingChanged(EventArgs e)
    {
        this.Invalidate();

      var handler = this.Events[_eventBarPaddingChanged] as EventHandler;

      handler?.Invoke(this, e);
    }

    /// <summary>
    /// Raises the <see cref="BarStyleChanged" /> event.
    /// </summary>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    protected virtual void OnBarStyleChanged(EventArgs e)
    {
        this.Invalidate();

      var handler = this.Events[_eventBarStyleChanged] as EventHandler;

      handler?.Invoke(this, e);
    }

    /// <summary>
    /// Raises the <see cref="Color1Changed" /> event.
    /// </summary>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    protected virtual void OnColor1Changed(EventArgs e)
    {
        this.Invalidate();

      var handler = (EventHandler)this.Events[_eventColor1Changed];

      handler?.Invoke(this, e);
    }

    /// <summary>
    /// Raises the <see cref="Color2Changed" /> event.
    /// </summary>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    protected virtual void OnColor2Changed(EventArgs e)
    {
        this.Invalidate();

      var handler = (EventHandler)this.Events[_eventColor2Changed];

      handler?.Invoke(this, e);
    }

    /// <summary>
    /// Raises the <see cref="Color3Changed" /> event.
    /// </summary>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    protected virtual void OnColor3Changed(EventArgs e)
    {
        this.Invalidate();

      var handler = this.Events[_eventColor3Changed] as EventHandler;

      handler?.Invoke(this, e);
    }

    /// <summary>
    /// Raises the <see cref="CustomColorsChanged" /> event.
    /// </summary>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    protected virtual void OnCustomColorsChanged(EventArgs e)
    {
        this.Invalidate();

      var handler = this.Events[_eventCustomColorsChanged] as EventHandler;

      handler?.Invoke(this, e);
    }

    /// <summary>
    /// Raises the <see cref="DividerStyleChanged" /> event.
    /// </summary>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    protected virtual void OnDividerStyleChanged(EventArgs e)
    {
        this.DefineBar();
      this.Invalidate();

      var handler = this.Events[_eventDividerStyleChanged] as EventHandler;

      handler?.Invoke(this, e);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.GotFocus" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
    protected override void OnGotFocus(EventArgs e)
    {
      base.OnGotFocus(e);

      this.Invalidate();
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.KeyDown" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
    protected override void OnKeyDown(KeyEventArgs e)
    {
      if (ColorSlider.IsNavigationKey(e.KeyCode))
      {
          e.Handled = true;

        var step = e.Shift
            ? _largeChange
            : _smallChange;
        var value = _value;

        switch (e.KeyCode)
        {
          case Keys.Right:
          case Keys.Down:
            value += step;
            break;

          case Keys.Left:
          case Keys.Up:
            value -= step;
            break;

          case Keys.PageDown:
            value += _largeChange;
            break;

          case Keys.PageUp:
            value -= _largeChange;
            break;

          case Keys.Home:
            value = _minimum;
            break;

          case Keys.End:
            value = _maximum;
            break;

          default:
            e.Handled = false;
            break;
        }

        this.Value = this.ClampValue(value);
      }

      base.OnKeyDown(e);
    }

    /// <summary>
    /// Raises the <see cref="LargeChangeChanged" /> event.
    /// </summary>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    protected virtual void OnLargeChangeChanged(EventArgs e)
    {
        var handler = this.Events[_eventLargeChangeChanged] as EventHandler;

      handler?.Invoke(this, e);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.LostFocus" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
    protected override void OnLostFocus(EventArgs e)
    {
      base.OnLostFocus(e);

      this.Invalidate();
    }

    /// <summary>
    /// Raises the <see cref="MaximumChanged" /> event.
    /// </summary>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    protected virtual void OnMaximumChanged(EventArgs e)
    {
        this.Invalidate();

      var handler = this.Events[_eventMaximumChanged] as EventHandler;

      handler?.Invoke(this, e);
    }

    /// <summary>
    /// Raises the <see cref="MinimumChanged" /> event.
    /// </summary>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    protected virtual void OnMinimumChanged(EventArgs e)
    {
        var handler = this.Events[_eventMinimumChanged] as EventHandler;

      handler?.Invoke(this, e);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);

      if (!this.Focused && this.TabStop)
      {
        this.Focus();
      }

      if (e.Button == MouseButtons.Left)
      {
        this.PointToValue(e.Location);
      }
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.MouseMove" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);

      if (e.Button == MouseButtons.Left)
      {
        this.PointToValue(e.Location);
      }
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.MouseWheel"/> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs"/> that contains the event data. </param>
    protected override void OnMouseWheel(MouseEventArgs e)
    {
      base.OnMouseWheel(e);

      this.Value = this.ClampValue(_value + -(e.Delta / SystemInformation.MouseWheelScrollDelta * SystemInformation.MouseWheelScrollLines));
    }

    /// <summary>
    /// Raises the <see cref="NubColorChanged" /> event.
    /// </summary>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    protected virtual void OnNubColorChanged(EventArgs e)
    {
        this.Invalidate();

      var handler = this.Events[_eventNubColorChanged] as EventHandler;

      handler?.Invoke(this, e);
    }

    /// <summary>
    /// Raises the <see cref="NubOutlineColorChanged" /> event.
    /// </summary>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    protected virtual void OnNubOutlineColorChanged(EventArgs e)
    {
        this.Invalidate();

      var handler = this.Events[_eventNubOutlineColorChanged] as EventHandler;

      handler?.Invoke(this, e);
    }

    /// <summary>
    /// Raises the <see cref="NubSizeChanged" /> event.
    /// </summary>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    protected virtual void OnNubSizeChanged(EventArgs e)
    {
        this.DefineBar();
      this.Invalidate();

      var handler = this.Events[_eventNubSizeChanged] as EventHandler;

      handler?.Invoke(this, e);
    }

    /// <summary>
    /// Raises the <see cref="NubStyleChanged" /> event.
    /// </summary>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    protected virtual void OnNubStyleChanged(EventArgs e)
    {
        this.DefineBar();
      this.Invalidate();

      var handler = this.Events[_eventNubStyleChanged] as EventHandler;

      handler?.Invoke(this, e);
    }

    /// <summary>
    /// Raises the <see cref="OrientationChanged" /> event.
    /// </summary>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    protected virtual void OnOrientationChanged(EventArgs e)
    {
        this.DefineBar();
      this.Invalidate();

      var handler = this.Events[_eventOrientationChanged] as EventHandler;

      handler?.Invoke(this, e);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.PaddingChanged" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
    protected override void OnPaddingChanged(EventArgs e)
    {
      base.OnPaddingChanged(e);

      this.DefineBar();
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);

      this.PaintBar(e);
      this.PaintAdornments(e);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.Resize" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
    protected override void OnResize(EventArgs e)
    {
      base.OnResize(e);

      this.DefineBar();
    }

    /// <summary>
    /// Raises the <see cref="ShowValueDividerChanged" /> event.
    /// </summary>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    protected virtual void OnShowValueDividerChanged(EventArgs e)
    {
        this.Invalidate();

      var handler = this.Events[_eventShowValueDividerChanged] as EventHandler;

      handler?.Invoke(this, e);
    }

    /// <summary>
    /// Raises the <see cref="SmallChangeChanged" /> event.
    /// </summary>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    protected virtual void OnSmallChangeChanged(EventArgs e)
    {
        var handler = this.Events[_eventSmallChangeChanged] as EventHandler;

      handler?.Invoke(this, e);
    }

    /// <summary>
    /// Raises the <see cref="ValueChanged" /> event.
    /// </summary>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    protected virtual void OnValueChanged(EventArgs e)
    {
        this.Refresh();

      var handler = this.Events[_eventValueChanged] as EventHandler;

      handler?.Invoke(this, e);
    }

    /// <summary>
    /// Paints control adornments.
    /// </summary>
    /// <param name="e">The <see cref="PaintEventArgs"/> instance containing the event data.</param>
    protected virtual void PaintAdornments(PaintEventArgs e)
    {
        var point = this.ValueToPoint(_value);

      // divider
      if (_showValueDivider)
      {
        Point start;
        Point end;

        if (_orientation == Orientation.Horizontal)
        {
          start = new Point(point.X, _barBounds.Top);
          end = new Point(point.X, _barBounds.Bottom);
        }
        else
        {
          start = new Point(_barBounds.Left, point.Y);
          end = new Point(_barBounds.Right, point.Y);
        }

        // draw a XOR'd line using Win32 API as this functionality isn't part of .NET
        PaintHelper.DrawInvertedLine(e.Graphics, start, end);
      }

      // focus
      if (this.Focused)
      {
        NativeMethods.DrawFocusRectangle(e.Graphics, Rectangle.Inflate(_barBounds, -2, -2));
      }

      // drag nub
      if (_nubStyle != ColorSliderNubStyle.None)
      {
        int x;
        int y;

        var hw = _nubSize.Width / 2 + 1;
        var hh = _nubSize.Height / 2 + 1;

        if (_orientation == Orientation.Horizontal)
        {
          x = point.X - hw + 1;
          y = _nubStyle == ColorSliderNubStyle.BottomRight
            ? _barBounds.Bottom - hh
            : _barBounds.Top - hh;
        }
        else
        {
          x = _nubStyle == ColorSliderNubStyle.BottomRight
            ? _barBounds.Right - hw
            : _barBounds.Left - hw;
          y = point.Y - hh + 1;
        }

        this.DrawNub(e.Graphics, x, y);
      }
    }

    /// <summary>
    /// Paints the bar.
    /// </summary>
    /// <param name="e">The <see cref="PaintEventArgs"/> instance containing the event data.</param>
    protected virtual void PaintBar(PaintEventArgs e)
    {
        // TODO: Should the final brush be cached?

      float angle = _orientation == Orientation.Horizontal
          ? 0
          : 90;

      if (_barBounds.Height > 0 && _barBounds.Width > 0)
      {
          var blend = new ColorBlend();

        switch (_barStyle)
        {
          case ColorBarStyle.TwoColor:
            blend.Colors = new[]
                           {
                               _color1,
                               _color2
                             };
            blend.Positions = _pairPositions;
            break;

          case ColorBarStyle.ThreeColor:
            blend.Colors = new[]
                           {
                               _color1,
                               _color2,
                               _color3
                             };
            blend.Positions = _triplePositions;
            break;

          case ColorBarStyle.Custom:

            if (_customColors != null && _customColors.Count > 0)
            {
                var count = _customColors.Count;
              blend.Colors = _customColors.ToArray();
              blend.Positions = Enumerable.Range(0, count).Select(i => i == 0 ? 0 : i == count - 1 ? 1 : (float)(1.0D / count) * i).ToArray();
            }
            else
            {
              blend.Colors = new[]
                             {
                                 _color1,
                                 _color2
                               };
              blend.Positions = _pairPositions;
            }
            break;
        }

        // HACK: Inflating the brush rectangle by 1 seems to get rid of a odd issue where the last color is drawn on the first pixel
        using (LinearGradientBrush brush = new LinearGradientBrush(Rectangle.Inflate(_barBounds, 1, 1), System.Drawing.Color.Empty, System.Drawing.Color.Empty, angle, false)
        {
          InterpolationColors = blend
        })
        {
          e.Graphics.FillRectangle(brush, _barBounds);
        }
      }
    }

    /// <summary>
    /// Computes the location of the specified client point into value coordinates.
    /// </summary>
    /// <param name="location">The client coordinate <see cref="Point"/> to convert.</param>
    protected virtual void PointToValue(Point location)
    {
        float value;

      var clientRectangle = this.ClientRectangle;
      location.X += clientRectangle.X - _barBounds.X;
      location.Y += clientRectangle.Y - _barBounds.Y;

      switch (_orientation)
      {
        case Orientation.Horizontal:
          value = _minimum + location.X / (float)_barBounds.Width * (_minimum + _maximum);
          break;

        default:
          value = _minimum + location.Y / (float)_barBounds.Height * (_minimum + _maximum);
          break;
      }

      this.Value = this.ClampValue(value);
    }

    /// <summary>
    /// Computes the location of the value point into client coordinates.
    /// </summary>
    /// <param name="value">The value coordinate <see cref="Point"/> to convert.</param>
    /// <returns>A <see cref="Point"/> that represents the converted <see cref="Point"/>, value, in client coordinates.</returns>
    protected virtual Point ValueToPoint(float value)
    {
        int x;
      int y;

      var padding = _barPadding + this.Padding;

      if (_orientation == Orientation.Horizontal)
      {
        x = Convert.ToInt32((_barBounds.Width - 1) / _maximum * value);
        y = 0;
      }
      else
      {
        x = 0;
        y = Convert.ToInt32((_barBounds.Height - 1) / _maximum * value);
      }

      return new Point(x + padding.Left, y + padding.Top);
    }

    private static bool IsNavigationKey(Keys keyData)
    {
      return (keyData & Keys.Left) == Keys.Left || (keyData & Keys.Up) == Keys.Up ||
             (keyData & Keys.Down) == Keys.Down || (keyData & Keys.Right) == Keys.Right ||
             (keyData & Keys.PageUp) == Keys.PageUp || (keyData & Keys.PageDown) == Keys.PageDown ||
             (keyData & Keys.Home) == Keys.Home || (keyData & Keys.End) == Keys.End;
    }

    private float ClampValue(float value)
    {
      if (value < _minimum)
      {
        value = _minimum;
      }

      if (value > _maximum)
      {
        value = _maximum;
      }

      return value;
    }

    private void DrawNub(Graphics g, int x, int y)
    {
        Point firstCorner;
      Point lastCorner;
      Point tipCorner;

      // TODO: Cache points and use graphics rotation to render

      if (_nubStyle == ColorSliderNubStyle.BottomRight)
      {
        lastCorner = new Point(x + _nubSize.Width, y + _nubSize.Height);

        if (_orientation == Orientation.Horizontal)
        {
          firstCorner = new Point(x, y + _nubSize.Height);
          tipCorner = new Point(x + _nubSize.Width / 2, y);
        }
        else
        {
          firstCorner = new Point(x + _nubSize.Width, y);
          tipCorner = new Point(x, y + _nubSize.Height / 2);
        }
      }
      else
      {
        firstCorner = new Point(x, y);

        if (_orientation == Orientation.Horizontal)
        {
          lastCorner = new Point(x + _nubSize.Width, y);
          tipCorner = new Point(x + _nubSize.Width / 2, y + _nubSize.Height);
        }
        else
        {
          lastCorner = new Point(x, y + _nubSize.Height);
          tipCorner = new Point(x + _nubSize.Width, y + _nubSize.Height / 2);
        }
      }

      // draw the shape
      var outer = new[]
      {
          firstCorner,
          lastCorner,
          tipCorner
      };

      g.SmoothingMode = SmoothingMode.AntiAlias;

      using (Brush brush = new SolidBrush(_nubColor))
      {
        g.FillPolygon(brush, outer);
      }

      using (Pen pen = new Pen(_nubOutlineColor))
      {
        g.DrawPolygon(pen, outer);
      }
    }
  }
}
