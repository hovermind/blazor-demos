using System;
using System.Collections.Generic;

namespace Spinner.Server.Components
{

    public struct StyleBuilder
    {
        private string stringBuffer;

        /// <summary>
        /// Creates a StyleBuilder used to define conditional in-line style used in a component. Call Build() to return the completed style as a string.
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="value"></param>
        public static StyleBuilder Default(string prop, string value) => new StyleBuilder(prop, value);

        /// <summary>
        /// Creates a StyleBuilder used to define conditional in-line style used in a component. Call Build() to return the completed style as a string.
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="value"></param>
        public static StyleBuilder Default(string style) => Empty().AddStyle(style);

        /// <summary>
        /// Creates a StyleBuilder used to define conditional in-line style used in a component. Call Build() to return the completed style as a string.
        /// </summary>
        public static StyleBuilder Empty() => new StyleBuilder();

        /// <summary>
        /// Creates a StyleBuilder used to define conditional in-line style used in a component. Call Build() to return the completed style as a string.
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="value"></param>
        public StyleBuilder(string prop, string value) => stringBuffer = stringBuffer = $"{prop}:{value};";

        /// <summary>
        /// Adds a conditional in-line style to the builder with space separator and closing semicolon.
        /// </summary>
        /// <param name="style"></param>
        public StyleBuilder AddStyle(string style) => !string.IsNullOrWhiteSpace(style) ? AddRaw($"{style};") : this;

        /// <summary>
        /// Adds a raw string to the builder that will be concatenated with the next style or value added to the builder.
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="value"></param>
        /// <returns>StyleBuilder</returns>
        private StyleBuilder AddRaw(string style)
        {
            stringBuffer += style;
            return this;
        }

        /// <summary>
        /// Adds a conditional in-line style to the builder with space separator and closing semicolon..
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="value">Style to add</param>
        /// <returns>StyleBuilder</returns>
        public StyleBuilder AddStyle(string prop, string value) => AddRaw($"{prop}:{value};");

        /// <summary>
        /// Adds a conditional in-line style to the builder with space separator and closing semicolon..
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="value">Style to conditionally add.</param>
        /// <param name="when">Condition in which the style is added.</param>
        /// <returns>StyleBuilder</returns>
        public StyleBuilder AddStyle(string prop, string value, bool when = true) => when ? this.AddStyle(prop, value) : this;


        /// <summary>
        /// Adds a conditional in-line style to the builder with space separator and closing semicolon..
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="value">Style to conditionally add.</param>
        /// <param name="when">Condition in which the style is added.</param>
        /// <returns></returns>
        public StyleBuilder AddStyle(string prop, Func<string> value, bool when = true) => when ? this.AddStyle(prop, value()) : this;

        /// <summary>
        /// Adds a conditional in-line style to the builder with space separator and closing semicolon..
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="value">Style to conditionally add.</param>
        /// <param name="when">Condition in which the style is added.</param>
        /// <returns>StyleBuilder</returns>
        public StyleBuilder AddStyle(string prop, string value, Func<bool> when = null) => this.AddStyle(prop, value, when());

        /// <summary>
        /// Adds a conditional in-line style to the builder with space separator and closing semicolon..
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="value">Style to conditionally add.</param>
        /// <param name="when">Condition in which the style is added.</param>
        /// <returns>StyleBuilder</returns>
        public StyleBuilder AddStyle(string prop, Func<string> value, Func<bool> when = null) => this.AddStyle(prop, value(), when());

        /// <summary>
        /// Adds a conditional nested StyleBuilder to the builder with separator and closing semicolon.
        /// </summary>
        /// <param name="builder">Style Builder to conditionally add.</param>
        /// <returns>StyleBuilder</returns>
        public StyleBuilder AddStyle(StyleBuilder builder) => this.AddRaw(builder.Build());

        /// <summary>
        /// Adds a conditional nested StyleBuilder to the builder with separator and closing semicolon.
        /// </summary>
        /// <param name="builder">Style Builder to conditionally add.</param>
        /// <param name="when">Condition in which the style is added.</param>
        /// <returns>StyleBuilder</returns>
        public StyleBuilder AddStyle(StyleBuilder builder, bool when = true) => when ? this.AddRaw(builder.Build()) : this;

        /// <summary>
        /// Adds a conditional in-line style to the builder with space separator and closing semicolon..
        /// </summary>
        /// <param name="builder">Style Builder to conditionally add.</param>
        /// <param name="when">Condition in which the styles are added.</param>
        /// <returns>StyleBuilder</returns>
        public StyleBuilder AddStyle(StyleBuilder builder, Func<bool> when = null) => this.AddStyle(builder, when());

        /// <summary>
        /// Adds a conditional in-line style to the builder with space separator and closing semicolon..
        /// A ValueBuilder action defines a complex set of values for the property.
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="builder"></param>
        /// <param name="when"></param>
        public StyleBuilder AddStyle(string prop, Action<ValueBuilder> builder, bool when = true)
        {
            ValueBuilder values = new ValueBuilder();
            builder(values);
            return AddStyle(prop, values.ToString(), when && values.HasValue);
        }

        /// <summary>
        /// Adds a conditional in-line style when it exists in a dictionary to the builder with separator.
        /// Null safe operation.
        /// </summary>
        /// <param name="additionalAttributes">Additional Attribute splat parameters</param>
        /// <returns>StyleBuilder</returns>
        public StyleBuilder AddStyleFromAttributes(IReadOnlyDictionary<string, object> additionalAttributes) =>
            additionalAttributes == null ? this :
            additionalAttributes.TryGetValue("style", out var c) ? AddRaw(c.ToString()) : this;


        /// <summary>
        /// Finalize the completed Style as a string.
        /// </summary>
        /// <returns>string</returns>
        public string Build()
        {
            // String buffer finalization code
            return stringBuffer != null ? stringBuffer.Trim() : string.Empty;
        }

        // ToString should only and always call Build to finalize the rendered string.
        public override string ToString() => Build();
    }

    public class ValueBuilder
    {
        private string stringBuffer;

        public bool HasValue => !string.IsNullOrWhiteSpace(stringBuffer);
        /// <summary>
        /// Adds a space separated conditional value to a property.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="when"></param>
        /// <returns></returns>
        public ValueBuilder AddValue(string value, bool when = true) => when ? AddRaw($"{value} ") : this;
        public ValueBuilder AddValue(Func<string> value, bool when = true) => when ? AddRaw($"{value()} ") : this;

        private ValueBuilder AddRaw(string style)
        {
            stringBuffer += style;
            return this;
        }

        public override string ToString() => stringBuffer != null ? stringBuffer.Trim() : string.Empty;
    }

    public struct CssBuilder
    {
        private string stringBuffer;

        /// <summary>
        /// Creates a CssBuilder used to define conditional CSS classes used in a component.
        /// Call Build() to return the completed CSS Classes as a string. 
        /// </summary>
        /// <param name="value"></param>
        public static CssBuilder Default(string value) => new CssBuilder(value);

        /// <summary>
        /// Creates an Empty CssBuilder used to define conditional CSS classes used in a component.
        /// Call Build() to return the completed CSS Classes as a string. 
        /// </summary>
        public static CssBuilder Empty() => new CssBuilder();

        /// <summary>
        /// Creates a CssBuilder used to define conditional CSS classes used in a component.
        /// Call Build() to return the completed CSS Classes as a string. 
        /// </summary>
        /// <param name="value"></param>
        public CssBuilder(string value) => stringBuffer = value;

        /// <summary>
        /// Adds a raw string to the builder that will be concatenated with the next class or value added to the builder.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>CssBuilder</returns>
        public CssBuilder AddValue(string value)
        {
            stringBuffer += value;
            return this;
        }

        /// <summary>
        /// Adds a CSS Class to the builder with space separator.
        /// </summary>
        /// <param name="value">CSS Class to add</param>
        /// <returns>CssBuilder</returns>
        public CssBuilder AddClass(string value) => AddValue(" " + value);

        /// <summary>
        /// Adds a conditional CSS Class to the builder with space separator.
        /// </summary>
        /// <param name="value">CSS Class to conditionally add.</param>
        /// <param name="when">Condition in which the CSS Class is added.</param>
        /// <returns>CssBuilder</returns>
        public CssBuilder AddClass(string value, bool when = true) => when ? this.AddClass(value) : this;

        /// <summary>
        /// Adds a conditional CSS Class to the builder with space separator.
        /// </summary>
        /// <param name="value">CSS Class to conditionally add.</param>
        /// <param name="when">Condition in which the CSS Class is added.</param>
        /// <returns>CssBuilder</returns>
        public CssBuilder AddClass(string value, Func<bool> when = null) => this.AddClass(value, when());

        /// <summary>
        /// Adds a conditional CSS Class to the builder with space separator.
        /// </summary>
        /// <param name="value">Function that returns a CSS Class to conditionally add.</param>
        /// <param name="when">Condition in which the CSS Class is added.</param>
        /// <returns>CssBuilder</returns>
        public CssBuilder AddClass(Func<string> value, bool when = true) => when ? this.AddClass(value()) : this;

        /// <summary>
        /// Adds a conditional CSS Class to the builder with space separator.
        /// </summary>
        /// <param name="value">Function that returns a CSS Class to conditionally add.</param>
        /// <param name="when">Condition in which the CSS Class is added.</param>
        /// <returns>CssBuilder</returns>
        public CssBuilder AddClass(Func<string> value, Func<bool> when = null) => this.AddClass(value, when());

        /// <summary>
        /// Adds a conditional nested CssBuilder to the builder with space separator.
        /// </summary>
        /// <param name="value">CSS Class to conditionally add.</param>
        /// <param name="when">Condition in which the CSS Class is added.</param>
        /// <returns>CssBuilder</returns>
        public CssBuilder AddClass(CssBuilder builder, bool when = true) => when ? this.AddClass(builder.Build()) : this;

        /// <summary>
        /// Adds a conditional CSS Class to the builder with space separator.
        /// </summary>
        /// <param name="value">CSS Class to conditionally add.</param>
        /// <param name="when">Condition in which the CSS Class is added.</param>
        /// <returns>CssBuilder</returns>
        public CssBuilder AddClass(CssBuilder builder, Func<bool> when = null) => this.AddClass(builder, when());

        /// <summary>
        /// Adds a conditional CSS Class when it exists in a dictionary to the builder with space separator.
        /// Null safe operation.
        /// </summary>
        /// <param name="additionalAttributes">Additional Attribute splat parameters</param>
        /// <returns>CssBuilder</returns>
        public CssBuilder AddClassFromAttributes(IReadOnlyDictionary<string, object> additionalAttributes) =>
            additionalAttributes == null ? this :
            additionalAttributes.TryGetValue("class", out var c) ? AddClass(c.ToString()) : this;

        /// <summary>
        /// Finalize the completed CSS Classes as a string.
        /// </summary>
        /// <returns>string</returns>
        public string Build()
        {
            // String buffer finalization code
            return stringBuffer != null ? stringBuffer.Trim() : string.Empty;
        }

        // ToString should only and always call Build to finalize the rendered string.
        public override string ToString() => Build();

    }

    public static class BuilderExtensions
    {
        /// <summary>
        /// Used to convert a CssBuilder into a null when it is empty.
        /// Usage: class=null causes the attribute to be excluded when rendered.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns>string</returns>
        public static string NullIfEmpty(this CssBuilder builder) =>
            string.IsNullOrEmpty(builder.ToString()) ? null : builder.ToString();

        /// <summary>
        /// Used to convert a StyleBuilder into a null when it is empty.
        /// Usage: style=null causes the attribute to be excluded when rendered.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns>string</returns>
        public static string NullIfEmpty(this StyleBuilder builder) =>
            string.IsNullOrEmpty(builder.ToString()) ? null : builder.ToString();

        /// <summary>
        /// Used to convert a string.IsNullOrEmpty into a null when it is empty.
        /// Usage: attribute=null causes the attribute to be excluded when rendered.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns>string</returns>
        public static string NullIfEmpty(this string s) =>
            string.IsNullOrEmpty(s) ? null : s;

    }
}
