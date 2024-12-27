namespace TS.FW.GIGA.Managers
{
    public partial class InOutManager
    {
        public bool GetCY(CyUnit unit, CyState state)
        {
            //if (unit == CyUnit.FILM_CUTTING)
            //{
            //    return GetCY(CyUnit.FILM_CUTTING_PLATE, state)
            //        && GetCY(CyUnit.FILM_CUTTING_CLAMP_01, state.ToRev())
            //        && GetCY(CyUnit.FILM_CUTTING_CLAMP_02, state.ToRev());
            //}
            //else if (unit == CyUnit.GAP_PRESS)
            //{
            //    return GetCY(CyUnit.GAP_PRESS_LEFT, state)
            //        && GetCY(CyUnit.GAP_PRESS_RIGHT, state);
            //}
            //else
            {
                var on = $"X_{unit}_{state}";
                var off = $"X_{unit}_{state.ToRev()}";

                return this.ReadX(on) == true && this.ReadX(off) == false;
            }
        }

        public void SetCY(CyUnit unit, CyState state)
        {
            //if (unit == CyUnit.FILM_CUTTING)
            //{
            //    this.SetCY(CyUnit.FILM_CUTTING_PLATE, state);
            //    this.SetCY(CyUnit.FILM_CUTTING_CLAMP_01, state.ToRev());
            //    this.SetCY(CyUnit.FILM_CUTTING_CLAMP_02, state.ToRev());
            //}
            //else if (unit == CyUnit.GAP_PRESS)
            //{
            //    this.SetCY(CyUnit.GAP_PRESS_LEFT, state);
            //    this.SetCY(CyUnit.GAP_PRESS_RIGHT, state);
            //}
            //else
            {
                var on = $"{unit}_{state}";
                var off = $"{unit}_{state.ToRev()}";

                this.WriteY(false, off);
                this.WriteY(true, on);

                if (AP.IsSim)
                {
                    this.WriteX(true, $"X_{on}");
                    this.WriteX(false, $"X_{off}");

                    System.Threading.Thread.Sleep(20);
                }
            }
        }
    }
}
