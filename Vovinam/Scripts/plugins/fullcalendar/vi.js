!function(n) {
    "function" == typeof define && define.amd
        ? define(["jquery", "moment"], n)
        : "object" == typeof exports ? module.exports = n(require("jquery"), require("moment")) : n(jQuery, moment)
}(function(n, t) {
    !function() {
        var n = t.defineLocale("vi", {
            months:
                "Tháng 1 -_Tháng 2 -_Tháng 3 -_Tháng 4 -_Tháng 5 -_Tháng 6 -_Tháng 7 -_Tháng 8 -_Tháng 9 -_Tháng 10 -_Tháng 11 -_Tháng 12 -".split("_"),
            monthsShort: "Tháng 1_Tháng 2_Tháng 3_Tháng 4_Tháng 5_Tháng 6_Tháng 7_Tháng 8_Tháng 9_Tháng 10_Tháng 11_Tháng 12".split("_"),
            monthsParseExact: !0,
            weekdays: "chủ nhật_thứ hai_thứ ba_thứ tư_thứ năm_thứ sáu_thứ bảy".split("_"),
            weekdaysShort: "CN_T2_T3_T4_T5_T6_T7".split("_"),
            weekdaysMin: "CN_T2_T3_T4_T5_T6_T7".split("_"),
            weekdaysParseExact: !0,
            meridiemParse: /sa|ch/i,
            isPM: function(n) { return /^ch$/i.test(n) },
            meridiem: function(n, t, e) { return n < 12 ? e ? "sa" : "SA" : e ? "ch" : "CH" },
            longDateFormat: { LT: "HH:mm", LTS: "HH:mm:ss", L: "DD/MM/YYYY", LL: "D MMMM [năm] YYYY", LLL: "D MMMM [năm] YYYY HH:mm", LLLL: "dddd, D MMMM [năm] YYYY HH:mm", l: "DD/M/YYYY", ll: "D MMM YYYY", lll: "D MMM YYYY HH:mm", llll: "ddd, D MMM YYYY HH:mm" },
            calendar: { sameDay: "[Hôm nay lúc] LT", nextDay: "[Ngày mai lúc] LT", nextWeek: "dddd [tuần tới lúc] LT", lastDay: "[Hôm qua lúc] LT", lastWeek: "dddd [tuần rồi lúc] LT", sameElse: "L" },
            relativeTime: { future: "%s tới", past: "%s trước", s: "vài giây", m: "một phút", mm: "%d phút", h: "một giờ", hh: "%d giờ", d: "một ngày", dd: "%d ngày", M: "một tháng", MM: "%d tháng", y: "một năm", yy: "%d năm" },
            ordinalParse: /\d{1,2}/,
            ordinal: function(n) { return n },
            week: { dow: 1, doy: 4 }
        });
        return n
    }()
});