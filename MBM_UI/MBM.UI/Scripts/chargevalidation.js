$(document).ready(function () {
    $('.maskedExt').on({
        keypress: function (ev) {
            if (ev.which == 46 || (ev.which > 47 && ev.which < 58)) {
                var chargeamount = ($(this).val().split('.'));
                //if (parseFloat($(this).val() + ev.key) >= 10000) {
                //    if (!confirm("you are entering greater than 10000")) {
                //        ev.preventDefault();
                //        return false;
                //    }
                //}
                if (chargeamount.length > 2) {
                    ev.preventDefault();
                    return false;
                }
                if ((ev.which == 46) && (chargeamount[0] == '')) {
                    $(this).val('0');
                }
                else if ((chargeamount.length > 1) && (chargeamount[1].length > 1)) {
                    //var newchargeamount = parseFloat($(this).val());

                    //newchargeamount = newchargeamount + parseFloat('0.00' + ev.key);
                    ev.preventDefault();
                }
                return true;
            }
            ev.preventDefault();
        },
        blur: function () {
            if (Number($(this).val() )< 0) {
                alert('Charge amount should be graterthan 0');
                return false;
            }
        }
    });
});