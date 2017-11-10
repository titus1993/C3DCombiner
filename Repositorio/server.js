
var http = require('http');
var url = require('url');
var fs = require('fs');
var express = require('express');
var bodyParser = require('body-parser');
var request = require('request');
var sql = require("mssql");
var dialog = require("dialog");
var ejs = require('ejs');

var app = express();

var config = {
    user: "C3DCombiner",
    password: "c3dcombiner",
    server: "localhost",
    database: "C3DCombiner",
};

var usuarioActual;
var idUsuarioActual;
var nombreUsuarioActual;

app.use('/bower_components', express.static('bower_components'));
app.use('/plugins', express.static('plugins'));
app.use('/dist', express.static('dist'));
app.use('/build', express.static('build'));
app.use(bodyParser.urlencoded({ extended: true })); 

app.set('views', __dirname + '/');
app.engine('html', ejs.renderFile);
app.set('view engine', 'html');


var server = app.listen(1993, function () {
    console.log('Servidor funcionando');
});


app.get('/', function (req, res) {
    if (usuarioActual == undefined) {
        res.render("index.html", { mensaje: "Registrate para iniciar sesion", usuario: "" });
    } else {
        res.redirect('/C3DCombiner');
    }
});

app.get('/register', function (req, res) {
    res.render("register.html", { username: "", usermail: "", mensaje: "Registrar una nueva cuenta" });
});

app.post('/', function (req, res) {
    var usuario = req.body.inUsuario.trim();
    var clave = req.body.inClave.trim();

    var conn = new sql.ConnectionPool(config);
    var req = new sql.Request(conn);

    usuarioActual = undefined;
    idUsuarioActual = undefined;
    nombreUsuarioActual = undefined;
    
    conn.connect(function (err) {
        if (err) {
            console.log(err);
            return;
        }
        req.query("SELECT * FROM USUARIO WHERE username= '" + usuario + "' and clave= '" + clave + "'", function (err, recordset) {
            if (err) {
                console.log(err);
                return;
            } else {
                if (recordset.rowsAffected[0] > 0) {
                    usuarioActual = usuario;
                    idUsuarioActual = recordset.recordset[0].id;
                    nombreUsuarioActual = recordset.recordset[0].nombre;
                    res.redirect('/C3DCombiner');
                } else {
                    res.render('index.html', { mensaje: "Usuario y/o contraseña incorrecta", usuario: usuario });
                }
            }
            conn.close();
        });
    }, err => { console.log(err) });
});

app.post('/registro', function (req, res) {
    var usuario = req.body.inUsername.trim();
    var password = req.body.inClave1.trim();
    var password2 = req.body.inClave2.trim();
    var nombre = req.body.inCorreo.trim();


    if (usuario != "" || password != "" || password2 != "" || nombre != "") {

        if (password == password2) {
            var conn = new sql.ConnectionPool(config);
            var req = new sql.Request(conn);

            conn.connect(function (err) {
                if (err) {
                    console.log(err);
                    return;
                }
                req.query("INSERT INTO USUARIO VALUES ('" + usuario + "', '" + password + "', '" + nombre + "')", function (err, recordset) {
                    if (err) {
                        res.render('register.html', { mensaje: "El nombre de usuario ya existe, ingrese otro", username: "", usermail: nombre });
                        return;
                    } else {
                        res.render("index.html", { mensaje: "Registrate para iniciar sesion", usuario: usuario });
                    }
                    conn.close();
                });
            });
        } else {
            res.render("register.html", { username: usuario, usermail: nombre, mensaje: "Las contraseñas deben ser iguales" })
        }

    } else {
        res.render("register.html", { username: usuario, usermail: nombre, mensaje: "Complete todos los campos" })
    }
});

app.get('/C3DCombiner', function (req, res) {
    var conn = new sql.ConnectionPool(config);
    var req = new sql.Request(conn);

    var data;
    if (usuarioActual == undefined) {
        res.redirect('/');
    } else {
        conn.connect(function (err) {
            if (err) {
                console.log(err);
                return;
            }
            req.query("SELECT * FROM USUARIO, ARCHIVO WHERE Usuario.id = archivo.usuario", function (err, recordset) {
                if (err) {
                    console.log(err);
                    return;
                } else {
                    if (usuarioActual !== undefined) {
                        data = recordset.recordset;
                        res.render("C3DCombiner.html", { datos: data, usuario: usuarioActual, nombre: nombreUsuarioActual, id: idUsuarioActual });
                    }

                }
                conn.close();
            });
        });
    }
});


app.get('/logout', function (req, res) {
    usuarioActual = undefined;
    idUsuarioActual = undefined;
    nombreUsuarioActual = undefined;

    res.redirect('/');
});

app.get('/eliminar', function (req, res) {
    var idClase = req.query['id'];

    var conn = new sql.ConnectionPool(config);
    var req = new sql.Request(conn);

    conn.connect(function (err) {
        if (err) {
            console.log(err);
            return;
        }
        req.query("DELETE ARCHIVO WHERE id = " + idClase, function (err, recordset) {
            if (err) {
                console.log(err);
                return;
            } else {
                res.redirect('/C3DCombiner');

            }
            conn.close();
        });
    });

});


app.get('/Archivo', function (req, res) {
    if (usuarioActual == undefined){
        res.redirect('/');
    } else {
        var idClase = req.query['id'];

        var conn = new sql.ConnectionPool(config);
        var req = new sql.Request(conn);

        conn.connect(function (err) {
            if (err) {
                console.log(err);
                return;
            }
            req.query("SELECT * FROM USUARIO, ARCHIVO WHERE archivo.id = " + idClase + " AND usuario.id = archivo.usuario", function (err, recordset) {
                if (err) {
                    console.log(err);
                    return;
                } else {
                    var data = recordset.recordset;
                    res.render("Archivo.html", { datos: data, usuario: usuarioActual, nombre: nombreUsuarioActual, id: idUsuarioActual });
                }
                conn.close();
            });
        });
    }    
});
