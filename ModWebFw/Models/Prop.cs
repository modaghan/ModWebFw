using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModWebFw
{
    public class Prop
    {
        public Prop(Tip tip, string name, string label)
        {
            this.Tip = tip;
            this.Name = name;
            this.Label = label;
        }
        public Prop(string name, string label, IQueryable<object> list, string shownProperty, string modalId = "", bool isMultiple = false)
        {
            this.Name = name;
            this.Label = label;
            this.List = list;
            this.ShownProperty = shownProperty;
            this.IsMultiple = isMultiple;
            this.ModalId = modalId;
        }
        public Tip Tip { get; set; }
        public string Label { get; set; }
        public string Name { get; set; }
        public IQueryable<object> List { get; set; }
        public string ShownProperty { get; set; }
        public bool IsMultiple { get; set; }
        public string ModalId { get; set; }
    }
    public enum Tip
    {
        button,
        checkbox,
        color,
        date,
        email,
        file,
        hidden,
        image,
        month,
        number,
        password,
        radio,
        range,
        reset,
        search,
        submit,
        tel,
        text,
        time,
        url,
        week,
        datetime
    }
}
