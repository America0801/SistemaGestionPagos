<template>
  <div>
    <!-- Botón hamburguesa -->
    <button class="btn btn-primary m-2" type="button" data-bs-toggle="offcanvas" data-bs-target="#offcanvasMenu"
      aria-controls="offcanvasMenu">
      <i class="bi bi-list"></i>
    </button>

    <!-- Offcanvas SideNav -->
    <div class="offcanvas offcanvas-start text-bg-dark" tabindex="-1" id="offcanvasMenu"
      aria-labelledby="offcanvasMenuLabel">
      <div class="offcanvas-header">
        <h5 class="offcanvas-title" id="offcanvasMenuLabel">Menú</h5>
        <button type="button" class="btn-close btn-close-white" data-bs-dismiss="offcanvas"
          aria-label="Cerrar"></button>
      </div>
      <div class="offcanvas-body">
        <ul class="list-unstyled">
          <li class="mb-2">
            <a href="#" class="nav-link text-white d-flex align-items-center">
              <i class="bi bi-house-door me-2"></i> Inicio
            </a>
          </li>
          <li class="mb-2">
            <a href="#" class="nav-link text-white d-flex align-items-center">
              <i class="bi bi-people me-2"></i> Usuarios
            </a>
          </li>
          <li class="mb-2">
            <a href="#" class="nav-link text-white d-flex align-items-center">
              <i class="bi bi-box-seam me-2"></i> Productos
            </a>
          </li>
          <li class="mb-2">
            <a href="#" class="nav-link text-white d-flex align-items-center">
              <i class="bi bi-cart me-2"></i> Ventas
            </a>
          </li>
          <li class="mb-2">
            <a href="#" class="nav-link text-white d-flex align-items-center">
              <i class="bi bi-graph-up me-2"></i> Reportes
            </a>
          </li>
        </ul>
      </div>
    </div>
  </div>
  <RouterView />
</template>

<script>
export default {
  name: "SideNav",
};
</script>

<style scoped>
.offcanvas {
  width: 250px;
}

.nav-link {
  font-size: 1.1rem;
  transition: 0.3s;
}

.nav-link:hover {
  background-color: rgba(255, 255, 255, 0.1);
  border-radius: 8px;
}
</style>
