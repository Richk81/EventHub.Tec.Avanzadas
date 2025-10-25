# EventHub API - ASP.NET Core Web API

## Descripción

EventHub es una API RESTful desarrollada con **ASP.NET Core Web API** que permite gestionar **Eventos**, **Asistentes** y **Comentarios**.  
El proyecto sigue una arquitectura por capas, separando responsabilidades para lograr **escalabilidad**, **mantenibilidad** y **claridad en la estructura del código**.

Esta API incluye funcionalidades de:  
- Crear, listar, actualizar y eliminar eventos.  
- Gestión de asistentes por evento.  
- Gestión de comentarios por evento.  
- Paginación y filtrado en listados.  
- Soft delete para eliminar registros sin borrar datos de la base.  
- Documentación de endpoints con **Swagger/OpenAPI**.  

---

## Estructura del Proyecto y Buenas Prácticas

La arquitectura sigue el patrón **MVC/Arquitectura por Capas**:

### 1. Controladores (Controllers/)
**Propósito:** Punto de entrada HTTP. Mapeo de URL y métodos HTTP.  

**Implementación:**
- `EventosController.cs`  
- `AsistentesController.cs`  
- `ComentariosController.cs`  

✅ Los endpoints están correctamente separados por recurso (`GET`, `POST`, `PUT`, `DELETE`).  
✅ Sub-recursos implementados (`/eventos/{id}/asistentes`, `/eventos/{id}/comentarios`).  

---

### 2. Modelos o Entidades (Models/ o Entities/)
**Propósito:** Representación de los datos y estructura de la base de datos.  

**Implementación:**
- `Evento.cs`  
- `Asistente.cs`  
- `Comentario.cs`  

✅ Campos correctamente definidos, incluyendo fechas, IDs y relaciones (`EventoId`).  
✅ Soft delete implementado (`Eliminado`).  

---

### 3. DTOs (DTOs/)
**Propósito:** Transferencia de datos entre capas, tanto request como response, incluyendo paginación.  

**Implementación:**
- `EventoRequest.cs`, `EventoResponse.cs`, `PaginacionResponse<T>.cs`  
- `AsistenteRequest.cs`, `AsistenteResponse.cs`  
- `ComentarioRequest.cs`, `ComentarioResponse.cs`  

✅ Buen manejo de paginación a través de `PaginacionResponse<T>` y `MetaData`.  
✅ Separación clara entre entidades y lo que expone la API.  

---

### 4. Servicios (Services/)
**Propósito:** Lógica de negocio, validaciones, reglas y transformaciones entre DTOs y entidades.  

**Implementación:**
- `IEventosService.cs`, `EventosService.cs`  
- `IAsistenteService.cs`, `AsistentesService.cs`  
- `IComentarioService.cs`, `ComentariosService.cs`  

✅ Los controladores llaman solo a los servicios; la lógica de negocio está encapsulada.  
✅ Filtrado, ordenamiento, paginación y validaciones implementadas.  

---

### 5. Repositorios (Repositories/)
**Propósito:** Acceso a datos usando **Entity Framework Core**.  

**Implementación:**
- `IEventosRepository.cs`, `EventosRepository.cs`  
- Métodos: `GetEventosAsync`, `GetByIdAsync`, `AddAsync`, `UpdateAsync`, `DeleteAsync`  
- Métodos similares para Asistentes y Comentarios.  

✅ Acceso a datos desacoplado de la lógica de negocio.  

---

### 6. Configuración (Program.cs)
**Propósito:** Registro de servicios, repositorios, middleware, autenticación y Swagger.  

**Implementación:**
- Registro de servicios y repositorios en DI:  
```csharp
builder.Services.AddScoped<IEventosService, EventosService>();
builder.Services.AddScoped<IAsistenteService, AsistentesService>();
builder.Services.AddScoped<IComentarioService, ComentariosService>();

----

### 7. Buenas prácticas generales

**Arquitectura por capas clara**: Controllers → Services → Repositories → DB/Entities.

- Uso de DTOs para requests/responses.

- Controladores sin lógica de negocio.

- Soporte de paginación, filtrado y soft delete.

- Documentación de API con XML Comments y Swagger.

**Cómo ejecutar el proyecto**

- Clonar el repositorio

git clone https://github.com/tu-usuario/eventhub-api.git
cd eventhub-api


- Restaurar paquetes NuGet

dotnet restore


- Configurar la base de datos

Editar appsettings.json con la cadena de conexión correcta.

- Aplicar migraciones si se usa EF Core:

dotnet ef database update


- Ejecutar la API

dotnet run


- Probar en Swagger

Abrir en el navegador: https://localhost:{PUERTO}/swagger

Ver y probar todos los endpoints con documentación generada.

----

### 8. Cómo iniciar y probar la API:

**1. Iniciar el proyecto**:

- A) Abre la solución en Visual Studio 2022.

- B) Presiona Ctrl + F5 (o haz clic en ▶️ “Start Without Debugging”) para ejecutar la API.

Esto abrirá automáticamente el navegador con la documentación interactiva de Swagger en la siguiente dirección (por defecto):
🖥️ http://localhost:5168/swagger

**2. Autenticación (Obtener Token JWT)**

- En Swagger, busca el endpoint:

POST /api/Auth/login

- Haz clic en “Try it out”.

- En el cuerpo de la solicitud (Request body), ingresa las credenciales de acceso.
Ejemplo:

{
  "usuario": "admin",
  "password": "1234"
}

- Presiona Execute.

- Si las credenciales son correctas, obtendrás una respuesta 200 OK con un token JWT como este:

{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "expiraEn": "2025-10-25T20:03:58.3725535Z"
}

- Autorizarse en Swagger

Copia el valor del campo "token".

En la parte superior derecha de Swagger, haz clic en el botón Authorize 🔓.

En el campo que aparece, escribe: Bearer <TU_TOKEN>

- Haz clic en Authorize y luego en Close.
✅ Ahora ya estás autenticado y puedes realizar peticiones a los demás endpoints protegidos.

**3. Probar los endpoints protegidos**:

- A) Elige cualquiera de los endpoints del grupo de controladores (por ejemplo, /api/Asistentes, /api/Eventos, etc.).

- B) Haz clic en Try it out y luego en Execute.

- C) Si tu token es válido, verás las respuestas esperadas (códigos 200, 201, etc.).

- D) Si el token expiró o no lo agregaste correctamente, Swagger mostrará un error 401 Unauthorized.

----

### 9. AUTOR:

**Alumno**: Arroyo Juan José Ricardo

- ISET - Instituto Superior de Educación Tecnológica
- Técnicas Avanzadas de programación - Año 2025
- Docente: Prof. Ingeniero Ríos Pedro.