using System;
using City_Center.Page;
using static City_Center.Models.TorneoResultado;

namespace City_Center.ViewModels
{
    public class TorneoDetalleViewModel
    {
        #region Attributes

        #endregion

        #region Properties
        public TorneoDetalle td
        {
            get;
            set;
        }
        #endregion

        public TorneoDetalleViewModel(TorneoDetalle td)
        {
            this.td = td;


        }
    }
}
