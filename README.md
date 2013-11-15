MoqUnitTestExamples
===================

En este laboratorio voy a investigar moq, que es la libreria sobre mocks mas descargada desde NuGet con diferencia.

Existes diferentes tipos de objetos dobles (doble de un objeto real) en pruebas unitarias.

Al final todos resuelven una dependencia del objeto a probar pero su incidencia en el test es lo que los
diferencia.

Dummy: No tiene influencia en el test, son utilizados para pasar por parametro. 
Un ejemplo pude ser que el objeto que estamos probando necesite un log en su constructor y le pasamos 
una implementacion vacia de una interface ILooger.

Fake:  proporciona una versión más ligera de una dependencia lo que hace pasar el test con mejor rendimiento.
Un buen ejemplo es un hacer un fake de un repositorio para asi evitar la conexion a la base de datos,cortes etc. 
Se parece a un stub pero la diferencia es que el comportamiento del objeto a probar cambia segun los
valores devueltos por funciones o excepciones de un stub y de un fake no. 

Stub: influye en el comportamiento del objeto a probar. Por ejemplo un stub de una entidad usuario 
que devuelve siempre que su shopping country es USA, esto afectara al comportamiento de una clase de negocio al 
devolver los productos recomendados para el usuario.

Mock: puede influir en el comportamiento del objeto a probar pero su verdadero cometido es que forma parte de la verificacion del 
test y para ello tiene una expectativas que verifica que se han cumplidom, como por ejemplo
que un metodo, funcion o propiedad suyo ha sido invocado por el objeto a probar al menos una vez.
Como ejemplo si utilizamos el mismo ejemplo que en stub, podemos comprobar con el mock que la propiedad ShoppingCountry
del usuario ha sido invocada para el test.

Moq permite generar dinamicamente estos objetos y ser usados en las pruebas unitarias.

Conclusion Laboratorio
----------------------

Moq es una herramienta bastante sencilla de utlizar y flexible. Lo que haces se queda dentro del proyecto de test y no ensucias la 
solución con otros proyectos fakes o cosas asi.


