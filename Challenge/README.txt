API en .NET Core/Framework exponiendo 2 endpoints:
	a) El primero tiene que simular un login (parametrizar un único usuario válido) y retornar en cualquier caos una estructura con el resultado.
		-> Si el Usuario es válido devuelve un Token

	b) El segundo tiene que, consumiendo la API pública https://apis.datos.gob.ar/georef/api/provincias, retornar lat y long de una provincia por nombre (https://datosgobar.github.io/georef-ar-api/)
		-> Autenticar usando el token obtenido en el login

Usuario de Prueba: 
		User: TestingUser
		Pass: TestingPass

Log Path: \Challenge\Challenge\Logs\[Date]
