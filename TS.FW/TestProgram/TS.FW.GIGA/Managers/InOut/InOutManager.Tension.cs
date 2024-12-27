namespace TS.FW.GIGA.Managers
{
    public partial class InOutManager
    {
        public bool GetEpcState(EpcUnit unit, EpcState state)
        {
            //switch (unit)
            //{
            //    case EpcUnit.All: return this.GetEpcState(EpcUnit.Unwinder, state) && this.GetEpcState(EpcUnit.Rewinder, state);
            //    case EpcUnit.Unwinder:
            //        {
            //            switch (state)
            //            {
            //                case EpcState.CenterMove: return X_EPC_UNWINDER_CENTER;
            //                case EpcState.Deviation: return X_EPC_UNWINDER_DEVIATION;
            //                case EpcState.Overload: return X_EPC_UNWINDER_OVERLOAD;
            //                case EpcState.ActLimit: return X_EPC_UNWINDER_ACTUATTOR_LIMIT;
            //                case EpcState.AutoMode: return X_EPC_UNWINDER_AUTO_MODE;
            //                case EpcState.ManualMode: return X_EPC_UNWINDER_MANUAL_MODE;
            //            }
            //        }
            //        break;
            //    case EpcUnit.Rewinder:
            //        {
            //            switch (state)
            //            {
            //                case EpcState.CenterMove: return X_EPC_REWINDER_CENTER;
            //                case EpcState.Deviation: return X_EPC_REWINDER_DEVIATION;
            //                case EpcState.Overload: return X_EPC_REWINDER_OVERLOAD;
            //                case EpcState.ActLimit: return X_EPC_REWINDER_ACTUATTOR_LIMIT;
            //                case EpcState.AutoMode: return X_EPC_REWINDER_AUTO_MODE;
            //                case EpcState.ManualMode: return X_EPC_REWINDER_MANUAL_MODE;
            //            }
            //        }
            //        break;
            //}

            return false;
        }

        public EpcMode GetEpcMode(EpcUnit unit, EpcMode mode)
        {
            if (this.GetEpcState(unit, EpcState.AutoMode)) return EpcMode.Auto;
            if (this.GetEpcState(unit, EpcState.ManualMode)) return EpcMode.Manual;
            if (this.GetEpcState(unit, EpcState.CenterMove)) return EpcMode.Center;

            return EpcMode.None;
        }

        public void SetEpcMode(EpcUnit unit, EpcMode mode)
        {
            switch (unit)
            {
                case EpcUnit.All:
                    {
                        this.SetEpcMode(EpcUnit.Unwinder, mode);
                        this.SetEpcMode(EpcUnit.Rewinder, mode);
                    }
                    break;
                case EpcUnit.Unwinder:
                    {
                        //this.EPC_UNWINDER_AUTO_MODE = mode == EpcMode.Auto;
                        //this.EPC_UNWINDER_MANUAL_MODE = mode == EpcMode.Manual;
                        //this.EPC_UNWINDER_CENTER = mode == EpcMode.Center;

                        //if (AP.IsSim)
                        //{
                        //    this.X_EPC_UNWINDER_AUTO_MODE = mode == EpcMode.Auto;
                        //    this.X_EPC_UNWINDER_MANUAL_MODE = mode == EpcMode.Manual;
                        //    this.X_EPC_UNWINDER_CENTER = mode == EpcMode.Center;
                        //}
                    }
                    break;
                case EpcUnit.Rewinder:
                    {
                        //this.EPC_REWINDER_AUTO_MODE = mode == EpcMode.Auto;
                        //this.EPC_REWINDER_MANUAL_MODE = mode == EpcMode.Manual;
                        //this.EPC_REWINDER_CENTER = mode == EpcMode.Center;

                        //if (AP.IsSim)
                        //{
                        //    this.X_EPC_REWINDER_AUTO_MODE = mode == EpcMode.Auto;
                        //    this.X_EPC_REWINDER_MANUAL_MODE = mode == EpcMode.Manual;
                        //    this.X_EPC_REWINDER_CENTER = mode == EpcMode.Center;
                        //}
                    }
                    break;
            }
        }

        public bool GetAirShaft(TensionUnit unit)
        {
            switch (unit)
            {
                case TensionUnit.All:
                    return this.GetAirShaft(TensionUnit.Unwinder)
                        && this.GetAirShaft(TensionUnit.Rewinder)
                        && this.GetAirShaft(TensionUnit.CoverFilm);
                    //case TensionUnit.Unwinder: return this.AIR_SHAFT_UNWINDER;
                    //case TensionUnit.Rewinder: return this.AIR_SHAFT_REWINDER;
                    //case TensionUnit.CoverFilm: return this.AIR_SHAFT_COVER_FILM;
            }

            return false;
        }

        public void SetAirShaft(TensionUnit unit, bool onoff)
        {
            switch (unit)
            {
                case TensionUnit.All:
                    {
                        this.SetAirShaft(TensionUnit.Unwinder, onoff);
                        this.SetAirShaft(TensionUnit.Rewinder, onoff);
                        this.SetAirShaft(TensionUnit.CoverFilm, onoff);
                    }
                    break;
                    //case TensionUnit.Unwinder: this.AIR_SHAFT_UNWINDER = onoff; break;
                    //case TensionUnit.Rewinder: this.AIR_SHAFT_REWINDER = onoff; break;
                    //case TensionUnit.CoverFilm: this.AIR_SHAFT_COVER_FILM = onoff; break;
            }
        }

        public bool GetTensionAuto(TensionUnit unit)
        {
            switch (unit)
            {
                case TensionUnit.All:
                    return this.GetTensionAuto(TensionUnit.Unwinder)
                        && this.GetTensionAuto(TensionUnit.Rewinder)
                        && this.GetTensionAuto(TensionUnit.CoverFilm);
                    //case TensionUnit.Unwinder: return this.TENSION_REWINDER_AUTO;
                    //case TensionUnit.Rewinder: return this.TENSION_REWINDER_AUTO;
                    //case TensionUnit.CoverFilm: return this.TENSION_COVER_FILM_RUN;
            }

            return false;
        }

        public void SetTensionAuto(TensionUnit unit, bool onoff)
        {
            switch (unit)
            {
                case TensionUnit.All:
                    {
                        this.SetTensionAuto(TensionUnit.Unwinder, onoff);
                        this.SetTensionAuto(TensionUnit.Rewinder, onoff);
                        this.SetTensionAuto(TensionUnit.CoverFilm, onoff);
                    }
                    break;
                    //case TensionUnit.Unwinder: this.TENSION_UNWINDER_AUTO = onoff; break;
                    //case TensionUnit.Rewinder: this.TENSION_REWINDER_AUTO = onoff; break;
                    //case TensionUnit.CoverFilm: this.TENSION_COVER_FILM_RUN = onoff; break;
            }
        }

        public bool GetTensionOut(TensionUnit unit)
        {
            switch (unit)
            {
                case TensionUnit.All:
                    return this.GetTensionOut(TensionUnit.Unwinder)
                        && this.GetTensionOut(TensionUnit.Rewinder)
                        && this.GetTensionOut(TensionUnit.CoverFilm);
                    //case TensionUnit.Unwinder: return this.TENSION_UNWINDER_OUT;
                    //case TensionUnit.Rewinder: return this.TENSION_REWINDER_OUT;
                    //case TensionUnit.CoverFilm: return this.TENSION_COVER_FILM_OUT;
            }

            return false;
        }

        public void SetTensionOut(TensionUnit unit, bool onoff)
        {
            switch (unit)
            {
                case TensionUnit.All:
                    {
                        this.SetTensionOut(TensionUnit.Unwinder, onoff);
                        this.SetTensionOut(TensionUnit.Rewinder, onoff);
                        this.SetTensionOut(TensionUnit.CoverFilm, onoff);
                    }
                    break;
                    //case TensionUnit.Unwinder: this.TENSION_UNWINDER_OUT = onoff; break;
                    //case TensionUnit.Rewinder: this.TENSION_REWINDER_OUT = onoff; break;
                    //case TensionUnit.CoverFilm: this.TENSION_COVER_FILM_OUT = onoff; break;
            }
        }

        public bool GetFilmCheck(TensionUnit unit)
        {
            switch (unit)
            {
                case TensionUnit.All:
                    return this.GetFilmCheck(TensionUnit.Unwinder)
                        && this.GetFilmCheck(TensionUnit.Rewinder)
                        && this.GetFilmCheck(TensionUnit.CoverFilm);
                    //case TensionUnit.Unwinder: return this.X_UNWINDER_FILM_CHECK_SENSER;
                    //case TensionUnit.Rewinder: return this.X_REWINDER_FILM_CHECK_SENSER;
                    //case TensionUnit.CoverFilm: return this.X_COVER_FILM_FILM_CHECK_SENSER;
            }

            return false;
        }
    }
}
