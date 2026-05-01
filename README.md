# TaskManagerApp - Blazor Web Apps Render Modes Demo

Este proyecto es una aplicación de ejemplo construida para la serie de artículos sobre **Blazor Web Apps** en [juredev.com/blog](https://juredev.com/blog/).

Puedes leer la primera parte aquí: [Blazor Web Apps: ¿Evolución definitiva o cambio de paradigma?](https://juredev.com/blog/2026/04/blazor-web-apps-evolucion-definitiva-o-cambio-de-paradigma/)

El objetivo principal de esta aplicación es demostrar el uso práctico de los diferentes **Render Modes** introducidos en .NET 8/9/10.

## Render Modes Demostrados

La aplicación cuenta con tres secciones principales, cada una utilizando un modo de renderizado distinto para optimizar la experiencia del usuario y el rendimiento:

1.  **Home (`/`) - Static SSR**: Renderizado estático en el servidor. Ideal para contenido público y SEO. Sin conexión persistente (SignalR) ni descarga de WebAssembly.
2.  **Tareas (`/tasks`) - Interactive Server**: Gestión de tareas interactiva procesada íntegramente en el servidor a través de SignalR. Interactividad instantánea sin necesidad de descargar el payload de WebAssembly.
3.  **Analíticas (`/analytics`) - Interactive Auto**: Un híbrido que utiliza el servidor para la carga inicial (rápida) y descarga WebAssembly en segundo plano. Las siguientes interacciones se ejecutan localmente en el navegador.

## Tecnologías Utilizadas

*   .NET 10
*   Blazor Web App (Interactive Auto / Per-page interactivity)
*   C#
*   Bootstrap (CSS)

## Cómo ejecutar el proyecto

1.  Asegúrate de tener instalado el SDK de .NET 10.
2.  Clona este repositorio.
3.  Navega a la carpeta del servidor:
    ```bash
    cd TaskManagerApp/TaskManagerApp
    ```
4.  Ejecuta la aplicación:
    ```bash
    dotnet run
    ```
5.  Abre tu navegador en `https://localhost:5172` (o el puerto indicado en la consola).

---
Creado por [Jure](https://juredev.com)
