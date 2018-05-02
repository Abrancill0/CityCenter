using System;
using static City_Center.Models.PromocionesResultado;

namespace City_Center.ViewModels
{
    public class DetallepromocionesViewModel
    {
        #region Attributes

        #endregion

        #region Properties
        public PromocionesDetalle pd
        {
            get;
            set;
        }
        #endregion

        public DetallepromocionesViewModel(PromocionesDetalle pd)
        {
            this.pd = pd;
        }
    }
}
