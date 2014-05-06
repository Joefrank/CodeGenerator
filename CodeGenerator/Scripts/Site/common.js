String.prototype.Format = function (arr) {
    var count = arr.length;
    var temp = this;

    for (var i = 0; i < count; i++) {
        var pattern = '{' + i + '}';
        while (temp.indexOf(pattern) > -1) {
            temp = temp.replace(pattern, arr[i]);
        }
    }
    return temp;
};

String.prototype.trim = String.prototype.trim || function () {
    return this.replace(/^\s+/, '').replace(/\s+$/, '');
}
