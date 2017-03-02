//Example: Print all positive integer solutions to the equation a3  +  b3 = c3  + d3 
// where a, b, c, and d are integers between 1 and n. 

(function () {
    var timeS = process.hrtime();
    var number = 150;
    var sol = equationWithHashTable(number);
    var diff = process.hrtime(timeS);
    process.stdout.write(`${Object.keys({ equationWithHashTable: equationWithHashTable })[0]} takes ${diff[0]} seconds and ${diff[1]} nanoseconds\n`);
    process.stdout.write(`The number of solutions is ${sol} \n`);

    var timeS2 = process.hrtime();
    sol = equationOneValidD(number)
    var diff2 = process.hrtime(timeS2);
    process.stdout.write(`${Object.keys({ equationOneValidD: equationOneValidD })[0]} takes ${diff2[0]} seconds and ${diff2[1]} nanoseconds\n`);
    process.stdout.write(`The number of solutions is ${sol} \n`);

    var timeS3 = process.hrtime();
    sol = equationBruteForce(number);
    var diff3 = process.hrtime(timeS3);
    process.stdout.write(`${Object.keys({ equationBruteForce: equationBruteForce })[0]} takes ${diff3[0]} seconds and ${diff3[1]} nanoseconds\n`);
    process.stdout.write(`The number of solutions is ${sol} \n`);

    function equationWithHashTable(n) {
        var num = n;
        var res;
        mapTable = {};

        for (a = 0; a < num; a++) {
            for (b = 0; b < num; b++) {
                res = Math.pow(a, 3) + Math.pow(b, 3);
                var list = mapTable[res];

                mapTable[res] = (!list)
                    ? [{ 'a': a, 'b': b }]
                    : (function () {
                        list.push({ 'a': a, 'b': b });
                        return list;
                    })();
            }
        }
        var sol = 0;
        for (var r in mapTable) {
            var list = mapTable[r];
            var dim = list.length;
            for (var i = 0; i < dim; i++) {
                for (var j = 0; j < dim; j++) {
                    //console.log(`( a:${list[i].a},b:${list[i].b} )  --> ( c:${list[j].a}, d:${list[j].b} )`)
                    sol++;
                }
            }
        }

        return sol;
    }

    function equationOneValidD(n) {
        var num = n;
        var res1, res2, cPower, d;
        var list = [];
        for (a = 0; a < num; a++) {
            for (b = 0; b < num; b++) {
                for (c = 0; c < num; c++) {
                    res1 = Math.pow(a, 3) + Math.pow(b, 3);
                    cPower = Math.pow(c, 3);
                    d = Math.round(Math.pow(Math.abs(res1 - cPower), 1 / 3));
                    if (d >= num) continue;
                    res2 = cPower + Math.pow(d, 3);

                    if (res1 == res2) {
                        list.push({ 'a': a, 'b': b, 'c': c, 'd': d });
                        //console.log(`( a:${a},b:${b} ) --> ( c:${c}, d:${d} )`)
                    }
                }
            }
        }

        return list.length;

    }

    function equationBruteForce(n) {
        var num = n;
        var res1, res2;
        var list = [];
        for (a = 0; a < num; a++) {
            for (b = 0; b < num; b++) {
                for (c = 0; c < num; c++) {
                    for (d = 0; d < num; d++) {

                        res1 = Math.pow(a, 3) + Math.pow(b, 3);
                        res2 = Math.pow(c, 3) + Math.pow(d, 3);
                        if (res1 == res2) {
                            list.push({ 'a': a, 'b': b, 'c': c, 'd': d });
                            //console.log(a,b,c,d);
                        }
                    }
                }
            }
        }

        return list.length;
    }
}())