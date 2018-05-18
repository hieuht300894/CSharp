var defaultValue = {
    Click: 'click',
    Json_Type: 'json',
    Json_ContentType: 'application/json;charset=utf-8',
    Get: 'get',
    Post: 'post',
    Put: 'put',
    Delete: 'delete',
    Now: function () {
        var date = new Date();
        var day = date.getDate();       // yields date
        var month = date.getMonth() + 1;    // yields month (add one as '.getMonth()' is zero indexed)
        var year = date.getFullYear();  // yields year
        var hour = date.getHours();     // yields hours 
        var minute = date.getMinutes(); // yields minutes
        var second = date.getSeconds(); // yields seconds

        // After this construct a string with the above results as below
        //return year + "-" + month + "-" + day + " " + hour + ':' + minute + ':' + second;
        return date;
    }
};