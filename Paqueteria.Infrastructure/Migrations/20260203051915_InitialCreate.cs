using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Paqueteria.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "articulos",
                columns: table => new
                {
                    id_articulo = table.Column<Guid>(type: "uuid", nullable: false),
                    clave = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    descripcion = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    estatus = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_articulos", x => x.id_articulo);
                });

            migrationBuilder.CreateTable(
                name: "choferes",
                columns: table => new
                {
                    id_chofere = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    apellido_paterno = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    apellido_materno = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    estatus_generico = table.Column<string>(type: "text", nullable: false),
                    direccion = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    telefono = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    num_camion = table.Column<int>(type: "integer", nullable: false),
                    num_contenedor = table.Column<int>(type: "integer", nullable: false),
                    num_contenedor2 = table.Column<int>(type: "integer", nullable: false),
                    fecha_alta = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fecha_baja = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_choferes", x => x.id_chofere);
                });

            migrationBuilder.CreateTable(
                name: "estados",
                columns: table => new
                {
                    id_estado = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    acronimo_3 = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_estados", x => x.id_estado);
                });

            migrationBuilder.CreateTable(
                name: "seguros",
                columns: table => new
                {
                    id_seguro = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_seguros", x => x.id_seguro);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    id_usuario = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    usarname = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    rol = table.Column<string>(type: "text", nullable: false),
                    estatus = table.Column<string>(type: "text", nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fecha_ultimo_acceso = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.id_usuario);
                });

            migrationBuilder.CreateTable(
                name: "asignaciones",
                columns: table => new
                {
                    id_asignacion = table.Column<Guid>(type: "uuid", nullable: false),
                    id_chofer = table.Column<Guid>(type: "uuid", nullable: false),
                    fecha_asignacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    st1 = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    st2 = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    st3 = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    st4 = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    camion = table.Column<int>(type: "integer", nullable: false),
                    num_contenedor = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    num_contenedor2 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_asignaciones", x => x.id_asignacion);
                    table.ForeignKey(
                        name: "FK_asignaciones_choferes_id_chofer",
                        column: x => x.id_chofer,
                        principalTable: "choferes",
                        principalColumn: "id_chofere",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "municipios",
                columns: table => new
                {
                    id_municipio = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    estado = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_municipios", x => x.id_municipio);
                    table.ForeignKey(
                        name: "FK_municipios_estados_estado",
                        column: x => x.estado,
                        principalTable: "estados",
                        principalColumn: "id_estado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "bitacora_accesos",
                columns: table => new
                {
                    id_acceso = table.Column<Guid>(type: "uuid", nullable: false),
                    id_usuario = table.Column<Guid>(type: "uuid", nullable: true),
                    exito = table.Column<bool>(type: "boolean", nullable: false),
                    ip_cliente = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    user_agent = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    fecha_acceso = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bitacora_accesos", x => x.id_acceso);
                    table.ForeignKey(
                        name: "FK_bitacora_accesos_usuarios_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "usuarios",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "direcciones_guia_snapshot",
                columns: table => new
                {
                    id_direccion = table.Column<Guid>(type: "uuid", nullable: false),
                    calle = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    numero_exterior = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    numero_interior = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    colonia = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    codigo_postal = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: true),
                    localidad = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    id_municipio = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_direcciones_guia_snapshot", x => x.id_direccion);
                    table.ForeignKey(
                        name: "FK_direcciones_guia_snapshot_municipios_id_municipio",
                        column: x => x.id_municipio,
                        principalTable: "municipios",
                        principalColumn: "id_municipio",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "empresas",
                columns: table => new
                {
                    id_empresa = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    nombre_corto = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    rfc = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    calle = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    codigo_postal = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: true),
                    id_municipio = table.Column<Guid>(type: "uuid", nullable: true),
                    fecha_alta = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_empresas", x => x.id_empresa);
                    table.ForeignKey(
                        name: "FK_empresas_municipios_id_municipio",
                        column: x => x.id_municipio,
                        principalTable: "municipios",
                        principalColumn: "id_municipio",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "sucursales",
                columns: table => new
                {
                    id_sucursal = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    codigo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    es_matriz = table.Column<bool>(type: "boolean", nullable: false),
                    calle = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    colonia = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    numero_exterior = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    numero_interior = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    localidad = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    id_municipio = table.Column<Guid>(type: "uuid", nullable: true),
                    telefono = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    estatus = table.Column<int>(type: "integer", nullable: false),
                    ip_servidor_local = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sucursales", x => x.id_sucursal);
                    table.ForeignKey(
                        name: "FK_sucursales_municipios_id_municipio",
                        column: x => x.id_municipio,
                        principalTable: "municipios",
                        principalColumn: "id_municipio",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "bitacora_sistema",
                columns: table => new
                {
                    id_bitacora = table.Column<Guid>(type: "uuid", nullable: false),
                    id_sucursal = table.Column<Guid>(type: "uuid", nullable: false),
                    id_usuario = table.Column<Guid>(type: "uuid", nullable: true),
                    accion = table.Column<int>(type: "integer", nullable: false),
                    tabla_afectada = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    id_registro_afectado = table.Column<Guid>(type: "uuid", nullable: false),
                    valores_anteriores = table.Column<string>(type: "jsonb", nullable: true),
                    valores_nuevos = table.Column<string>(type: "jsonb", nullable: true),
                    ip_cliente = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    user_agent = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    fecha_evento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bitacora_sistema", x => x.id_bitacora);
                    table.ForeignKey(
                        name: "FK_bitacora_sistema_sucursales_id_sucursal",
                        column: x => x.id_sucursal,
                        principalTable: "sucursales",
                        principalColumn: "id_sucursal",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_bitacora_sistema_usuarios_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "usuarios",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "clientes",
                columns: table => new
                {
                    id_cliente = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    estatus = table.Column<int>(type: "integer", nullable: false),
                    rfc = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    direccion = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    direccion_complemento = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    codigo_postal = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: true),
                    id_municipio = table.Column<Guid>(type: "uuid", nullable: true),
                    telefono = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    telefono_2 = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    correo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    contacto = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    num_convenio = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    poliza_seguro = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    id_sucursal = table.Column<Guid>(type: "uuid", nullable: true),
                    fecha_alta = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clientes", x => x.id_cliente);
                    table.ForeignKey(
                        name: "FK_clientes_municipios_id_municipio",
                        column: x => x.id_municipio,
                        principalTable: "municipios",
                        principalColumn: "id_municipio",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_clientes_sucursales_id_sucursal",
                        column: x => x.id_sucursal,
                        principalTable: "sucursales",
                        principalColumn: "id_sucursal",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "rutas",
                columns: table => new
                {
                    id_ruta = table.Column<Guid>(type: "uuid", nullable: false),
                    sucursal_origen = table.Column<Guid>(type: "uuid", nullable: false),
                    sucursal_destino = table.Column<Guid>(type: "uuid", nullable: false),
                    descripcion = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    num_camion = table.Column<int>(type: "integer", nullable: true),
                    num_contenedor = table.Column<int>(type: "integer", nullable: true),
                    num_contenedor_2 = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rutas", x => x.id_ruta);
                    table.ForeignKey(
                        name: "FK_rutas_sucursales_sucursal_destino",
                        column: x => x.sucursal_destino,
                        principalTable: "sucursales",
                        principalColumn: "id_sucursal",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_rutas_sucursales_sucursal_origen",
                        column: x => x.sucursal_origen,
                        principalTable: "sucursales",
                        principalColumn: "id_sucursal",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "guias",
                columns: table => new
                {
                    id_guia = table.Column<Guid>(type: "uuid", nullable: false),
                    clave = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    forma_pago = table.Column<string>(type: "text", nullable: false),
                    fecha_captura = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fecha_envio = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    fecha_pago = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    cliente_origen = table.Column<Guid>(type: "uuid", nullable: false),
                    direccion_origen = table.Column<Guid>(type: "uuid", nullable: false),
                    cliente_destino = table.Column<Guid>(type: "uuid", nullable: false),
                    direccion_destino = table.Column<Guid>(type: "uuid", nullable: false),
                    sucursal_origen = table.Column<Guid>(type: "uuid", nullable: false),
                    sucursal_destino = table.Column<Guid>(type: "uuid", nullable: false),
                    usuario_alta = table.Column<Guid>(type: "uuid", nullable: true),
                    usuario_cobro = table.Column<Guid>(type: "uuid", nullable: true),
                    costo_flete = table.Column<decimal>(type: "numeric(15,2)", precision: 15, scale: 2, nullable: false),
                    iva = table.Column<decimal>(type: "numeric(15,2)", nullable: false),
                    ivar = table.Column<decimal>(type: "numeric(15,2)", nullable: false),
                    subtotal = table.Column<decimal>(type: "numeric(15,2)", nullable: false),
                    total = table.Column<decimal>(type: "numeric(15,2)", nullable: false),
                    cobro_seguro = table.Column<decimal>(type: "numeric(15,2)", nullable: false),
                    importe_texto = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    observaciones = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    poliza_seguro = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_guias", x => x.id_guia);
                    table.ForeignKey(
                        name: "FK_guias_clientes_cliente_destino",
                        column: x => x.cliente_destino,
                        principalTable: "clientes",
                        principalColumn: "id_cliente",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_guias_clientes_cliente_origen",
                        column: x => x.cliente_origen,
                        principalTable: "clientes",
                        principalColumn: "id_cliente",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_guias_direcciones_guia_snapshot_direccion_destino",
                        column: x => x.direccion_destino,
                        principalTable: "direcciones_guia_snapshot",
                        principalColumn: "id_direccion",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_guias_direcciones_guia_snapshot_direccion_origen",
                        column: x => x.direccion_origen,
                        principalTable: "direcciones_guia_snapshot",
                        principalColumn: "id_direccion",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_guias_sucursales_sucursal_destino",
                        column: x => x.sucursal_destino,
                        principalTable: "sucursales",
                        principalColumn: "id_sucursal",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_guias_sucursales_sucursal_origen",
                        column: x => x.sucursal_origen,
                        principalTable: "sucursales",
                        principalColumn: "id_sucursal",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_guias_usuarios_usuario_alta",
                        column: x => x.usuario_alta,
                        principalTable: "usuarios",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_guias_usuarios_usuario_cobro",
                        column: x => x.usuario_cobro,
                        principalTable: "usuarios",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "articulos_guia",
                columns: table => new
                {
                    id_articulos_guia = table.Column<Guid>(type: "uuid", nullable: false),
                    id_guia = table.Column<Guid>(type: "uuid", nullable: false),
                    id_articulo = table.Column<Guid>(type: "uuid", nullable: false),
                    cantidad = table.Column<int>(type: "integer", nullable: false),
                    peso = table.Column<decimal>(type: "numeric(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_articulos_guia", x => x.id_articulos_guia);
                    table.ForeignKey(
                        name: "FK_articulos_guia_articulos_id_articulo",
                        column: x => x.id_articulo,
                        principalTable: "articulos",
                        principalColumn: "id_articulo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_articulos_guia_guias_id_guia",
                        column: x => x.id_guia,
                        principalTable: "guias",
                        principalColumn: "id_guia",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "asignaciones_guias",
                columns: table => new
                {
                    id_asignacion_guia = table.Column<Guid>(type: "uuid", nullable: false),
                    id_asignacion = table.Column<Guid>(type: "uuid", nullable: false),
                    id_guia = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_asignaciones_guias", x => x.id_asignacion_guia);
                    table.ForeignKey(
                        name: "FK_asignaciones_guias_asignaciones_id_asignacion",
                        column: x => x.id_asignacion,
                        principalTable: "asignaciones",
                        principalColumn: "id_asignacion",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_asignaciones_guias_guias_id_guia",
                        column: x => x.id_guia,
                        principalTable: "guias",
                        principalColumn: "id_guia",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_articulos_guia_id_articulo",
                table: "articulos_guia",
                column: "id_articulo");

            migrationBuilder.CreateIndex(
                name: "IX_articulos_guia_id_guia",
                table: "articulos_guia",
                column: "id_guia");

            migrationBuilder.CreateIndex(
                name: "IX_asignaciones_id_chofer",
                table: "asignaciones",
                column: "id_chofer");

            migrationBuilder.CreateIndex(
                name: "IX_asignaciones_guias_id_asignacion",
                table: "asignaciones_guias",
                column: "id_asignacion");

            migrationBuilder.CreateIndex(
                name: "IX_asignaciones_guias_id_guia",
                table: "asignaciones_guias",
                column: "id_guia");

            migrationBuilder.CreateIndex(
                name: "IX_bitacora_accesos_id_usuario",
                table: "bitacora_accesos",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_bitacora_sistema_id_sucursal",
                table: "bitacora_sistema",
                column: "id_sucursal");

            migrationBuilder.CreateIndex(
                name: "IX_bitacora_sistema_id_usuario",
                table: "bitacora_sistema",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_clientes_id_municipio",
                table: "clientes",
                column: "id_municipio");

            migrationBuilder.CreateIndex(
                name: "IX_clientes_id_sucursal",
                table: "clientes",
                column: "id_sucursal");

            migrationBuilder.CreateIndex(
                name: "IX_direcciones_guia_snapshot_id_municipio",
                table: "direcciones_guia_snapshot",
                column: "id_municipio");

            migrationBuilder.CreateIndex(
                name: "IX_empresas_id_municipio",
                table: "empresas",
                column: "id_municipio");

            migrationBuilder.CreateIndex(
                name: "IX_guias_cliente_destino",
                table: "guias",
                column: "cliente_destino");

            migrationBuilder.CreateIndex(
                name: "IX_guias_cliente_origen",
                table: "guias",
                column: "cliente_origen");

            migrationBuilder.CreateIndex(
                name: "IX_guias_direccion_destino",
                table: "guias",
                column: "direccion_destino");

            migrationBuilder.CreateIndex(
                name: "IX_guias_direccion_origen",
                table: "guias",
                column: "direccion_origen");

            migrationBuilder.CreateIndex(
                name: "IX_guias_sucursal_destino",
                table: "guias",
                column: "sucursal_destino");

            migrationBuilder.CreateIndex(
                name: "IX_guias_sucursal_origen",
                table: "guias",
                column: "sucursal_origen");

            migrationBuilder.CreateIndex(
                name: "IX_guias_usuario_alta",
                table: "guias",
                column: "usuario_alta");

            migrationBuilder.CreateIndex(
                name: "IX_guias_usuario_cobro",
                table: "guias",
                column: "usuario_cobro");

            migrationBuilder.CreateIndex(
                name: "IX_municipios_estado",
                table: "municipios",
                column: "estado");

            migrationBuilder.CreateIndex(
                name: "IX_rutas_sucursal_destino",
                table: "rutas",
                column: "sucursal_destino");

            migrationBuilder.CreateIndex(
                name: "IX_rutas_sucursal_origen",
                table: "rutas",
                column: "sucursal_origen");

            migrationBuilder.CreateIndex(
                name: "IX_sucursales_id_municipio",
                table: "sucursales",
                column: "id_municipio");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "articulos_guia");

            migrationBuilder.DropTable(
                name: "asignaciones_guias");

            migrationBuilder.DropTable(
                name: "bitacora_accesos");

            migrationBuilder.DropTable(
                name: "bitacora_sistema");

            migrationBuilder.DropTable(
                name: "empresas");

            migrationBuilder.DropTable(
                name: "rutas");

            migrationBuilder.DropTable(
                name: "seguros");

            migrationBuilder.DropTable(
                name: "articulos");

            migrationBuilder.DropTable(
                name: "asignaciones");

            migrationBuilder.DropTable(
                name: "guias");

            migrationBuilder.DropTable(
                name: "choferes");

            migrationBuilder.DropTable(
                name: "clientes");

            migrationBuilder.DropTable(
                name: "direcciones_guia_snapshot");

            migrationBuilder.DropTable(
                name: "usuarios");

            migrationBuilder.DropTable(
                name: "sucursales");

            migrationBuilder.DropTable(
                name: "municipios");

            migrationBuilder.DropTable(
                name: "estados");
        }
    }
}
