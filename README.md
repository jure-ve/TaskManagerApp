# TaskManagerApp - Blazor Web Apps Render Modes Demo

Este proyecto es una aplicación de ejemplo construida para la serie de artículos sobre **Blazor Web Apps** en [juredev.com/blog](https://juredev.com/blog/).

Puedes leer la serie completa de artículos aquí:
1. [Blazor Web Apps: ¿Evolución definitiva o cambio de paradigma?](https://juredev.com/blog/2026/04/blazor-web-apps-evolucion-definitiva-o-cambio-de-paradigma/)
2. [Blazor Web Apps en la práctica: Elegir y aplicar Render Modes](https://juredev.com/blog/2026/05/blazor-web-apps-elegir-y-aplicar-render-modes/)
3. [Blazor Web Apps en profundidad: Estado, ciclo de vida y pitfalls](https://juredev.com/blog/2026/05/blazor-web-apps-en-profundidad-estado-ciclo-de-vida-y-pitfalls/)
4. [Blazor Web Apps: Arquitectura real, decisiones y trade-offs](https://juredev.com/blog/2026/05/blazor-web-apps-arquitectura-real-decisiones-y-trade-offs/)

El objetivo principal de esta aplicación es demostrar el uso práctico de los diferentes **Render Modes** introducidos en .NET 8/9/10.

## Arquitectura Real y Patrones (Artículo 4)

La aplicación implementa una arquitectura modular de grado profesional para soportar el modo *Interactive Auto*:

*   **Estructura Multi-proyecto**: Separación en proyectos `Server`, `Client` y `Shared` para una gestión limpia de dependencias.
*   **Inyección de Dependencias Híbrida**: Uso de la misma interfaz (`ITaskService`) con implementaciones automáticas según el entorno:
    *   `ServerTaskService`: Acceso directo a datos para ejecución en el servidor (latencia cero).
    *   `ClientTaskService`: Comunicación vía HTTP para ejecución en WebAssembly.
*   **BFF (Backend-For-Frontend)**: Implementación de Minimal APIs para exponer datos de forma segura al cliente WASM.
*   **Autenticación Híbrida**: Flujo de seguridad basado en Cookies `HttpOnly` en el servidor con transferencia de identidad a WASM mediante `PersistentComponentState` durante el prerendering.

## Render Modes Demostrados

La aplicación cuenta con tres secciones principales, cada una utilizando un modo de renderizado distinto para optimizar la experiencia del usuario y el rendimiento:

1.  **Home (`/`) - Static SSR**: Renderizado estático en el servidor. Ideal para contenido público y SEO. Sin conexión persistente (SignalR) ni descarga de WebAssembly.
2.  **Tareas (`/tasks`) - Interactive Server**: Gestión de tareas interactiva procesada íntegramente en el servidor a través de SignalR. Interactividad instantánea sin necesidad de descargar el payload de WebAssembly.
3.  **Analíticas (`/analytics`) - Interactive Auto**: Un híbrido que utiliza el servidor para la carga inicial (rápida) y descarga WebAssembly en segundo plano. Las siguientes interacciones se ejecutan localmente en el navegador. Demuestra el uso de `PersistentComponentState` para evitar la doble ejecución durante el prerendering.

## Manejo de Estado y Ciclo de Vida (Artículo 3)

Esta aplicación también ilustra conceptos avanzados de la arquitectura de Blazor:

*   **`RendererInfo`**: Se utiliza en la interfaz gráfica de `Tasks` y `Analytics` para visualizar en tiempo real en qué entorno se está ejecutando el componente (Prerendering, Server o WebAssembly).
*   **`PersistentComponentState`**: Implementado en `Analytics.razor` para hidratar el estado del cliente con los datos obtenidos en el servidor durante el Prerendering, evitando llamados redundantes a la base de datos y parpadeos en la interfaz.
*   **Pitfalls de Estado**: Comentarios didácticos en `Tasks.razor` que advierten sobre la volatilidad del estado almacenado en memoria bajo conexiones SignalR (`InteractiveServer`).

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
