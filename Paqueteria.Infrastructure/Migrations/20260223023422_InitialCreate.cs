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
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    clave = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    descripcion = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    estatus = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_articulos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "estados",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    acronimo_2 = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_estados", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "seguros",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_seguros", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    usuario = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    rol = table.Column<string>(type: "text", nullable: false),
                    estatus = table.Column<string>(type: "text", nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fecha_ultimo_acceso = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "municipios",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    estado = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_municipios", x => x.id);
                    table.ForeignKey(
                        name: "FK_municipios_estados_estado",
                        column: x => x.estado,
                        principalTable: "estados",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "bitacora_accesos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    usuario_id = table.Column<Guid>(type: "uuid", nullable: false),
                    exito = table.Column<bool>(type: "boolean", nullable: false),
                    cliente_ip = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bitacora_accesos", x => x.id);
                    table.ForeignKey(
                        name: "FK_bitacora_accesos_usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "choferes",
                columns: table => new
                {
                    chofer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    apellido_paterno = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    apellido_materno = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    estatus = table.Column<string>(type: "text", nullable: false),
                    calle = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    colonia = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    numero_exterior = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    numero_interior = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    localidad = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    id_municipio = table.Column<Guid>(type: "uuid", nullable: false),
                    telefono = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    num_camion = table.Column<int>(type: "integer", nullable: false),
                    num_contenedor = table.Column<int>(type: "integer", nullable: false),
                    num_contenedor2 = table.Column<int>(type: "integer", nullable: false),
                    fecha_alta = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fecha_baja = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_choferes", x => x.chofer_id);
                    table.ForeignKey(
                        name: "FK_choferes_municipios_id_municipio",
                        column: x => x.id_municipio,
                        principalTable: "municipios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "direcciones_guia_snapshot",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    calle = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    numero_exterior = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    numero_interior = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    colonia = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    codigo_postal = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    localidad = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    municipio_id = table.Column<Guid>(type: "uuid", nullable: false),
                    IdMunicipio = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_direcciones_guia_snapshot", x => x.id);
                    table.ForeignKey(
                        name: "FK_direcciones_guia_snapshot_municipios_IdMunicipio",
                        column: x => x.IdMunicipio,
                        principalTable: "municipios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "empresas",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    nombre_corto = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    rfc = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    calle = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    codigo_postal = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    municipio_id = table.Column<Guid>(type: "uuid", nullable: false),
                    fecha_alta = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_empresas", x => x.id);
                    table.ForeignKey(
                        name: "FK_empresas_municipios_municipio_id",
                        column: x => x.municipio_id,
                        principalTable: "municipios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "sucursales",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    codigo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    es_matriz = table.Column<bool>(type: "boolean", nullable: false),
                    calle = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    colonia = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    numero_exterior = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    numero_interior = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    localidad = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    municipio_id = table.Column<Guid>(type: "uuid", nullable: false),
                    telefono = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    estatus = table.Column<int>(type: "integer", nullable: false),
                    servidor_ip = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sucursales", x => x.id);
                    table.ForeignKey(
                        name: "FK_sucursales_municipios_municipio_id",
                        column: x => x.municipio_id,
                        principalTable: "municipios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "asignaciones",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    chofer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    fecha_asignacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    st1 = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    st2 = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    st3 = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    st4 = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    camion = table.Column<int>(type: "integer", nullable: false),
                    num_contenedor = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    num_contenedor2 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    IdChofer = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_asignaciones", x => x.id);
                    table.ForeignKey(
                        name: "FK_asignaciones_choferes_IdChofer",
                        column: x => x.IdChofer,
                        principalTable: "choferes",
                        principalColumn: "chofer_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "bitacora_sistema",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    sucursal_id = table.Column<Guid>(type: "uuid", nullable: false),
                    usuario_id = table.Column<Guid>(type: "uuid", nullable: false),
                    accion = table.Column<int>(type: "integer", nullable: false),
                    tabla = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    registro_id = table.Column<Guid>(type: "uuid", nullable: false),
                    valor_anterior = table.Column<string>(type: "jsonb", nullable: true),
                    valor_nuevo = table.Column<string>(type: "jsonb", nullable: true),
                    cliente_ip = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bitacora_sistema", x => x.id);
                    table.ForeignKey(
                        name: "FK_bitacora_sistema_sucursales_sucursal_id",
                        column: x => x.sucursal_id,
                        principalTable: "sucursales",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_bitacora_sistema_usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "clientes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    estatus = table.Column<int>(type: "integer", nullable: false),
                    rfc = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    direccion = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    direccion_complemento = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    codigo_postal = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: true),
                    municipio_id = table.Column<Guid>(type: "uuid", nullable: true),
                    telefono = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    telefono_2 = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    correo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    contacto = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    num_convenio = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    poliza_seguro = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    sucursal_id = table.Column<Guid>(type: "uuid", nullable: true),
                    fecha_alta = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clientes", x => x.id);
                    table.ForeignKey(
                        name: "FK_clientes_municipios_municipio_id",
                        column: x => x.municipio_id,
                        principalTable: "municipios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_clientes_sucursales_sucursal_id",
                        column: x => x.sucursal_id,
                        principalTable: "sucursales",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "rutas",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    sucursal_origen_id = table.Column<Guid>(type: "uuid", nullable: false),
                    sucursal_destino_id = table.Column<Guid>(type: "uuid", nullable: false),
                    descripcion = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    num_camion = table.Column<int>(type: "integer", nullable: true),
                    num_contenedor = table.Column<int>(type: "integer", nullable: true),
                    num_contenedor_2 = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rutas", x => x.id);
                    table.ForeignKey(
                        name: "FK_rutas_sucursales_sucursal_destino_id",
                        column: x => x.sucursal_destino_id,
                        principalTable: "sucursales",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_rutas_sucursales_sucursal_origen_id",
                        column: x => x.sucursal_origen_id,
                        principalTable: "sucursales",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "guias",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    clave = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    forma_pago = table.Column<string>(type: "text", nullable: false),
                    fecha_captura = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fecha_envio = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    fecha_pago = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    cliente_origen_id = table.Column<Guid>(type: "uuid", nullable: false),
                    direccion_origen_id = table.Column<Guid>(type: "uuid", nullable: false),
                    cliente_destino_id = table.Column<Guid>(type: "uuid", nullable: false),
                    direccion_destino_id = table.Column<Guid>(type: "uuid", nullable: false),
                    sucursal_origen_id = table.Column<Guid>(type: "uuid", nullable: false),
                    sucursal_destino_id = table.Column<Guid>(type: "uuid", nullable: false),
                    usuario_alta_id = table.Column<Guid>(type: "uuid", nullable: false),
                    usuario_cobro_id = table.Column<Guid>(type: "uuid", nullable: true),
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
                    table.PrimaryKey("PK_guias", x => x.id);
                    table.ForeignKey(
                        name: "FK_guias_clientes_cliente_destino_id",
                        column: x => x.cliente_destino_id,
                        principalTable: "clientes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_guias_clientes_cliente_origen_id",
                        column: x => x.cliente_origen_id,
                        principalTable: "clientes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_guias_direcciones_guia_snapshot_direccion_destino_id",
                        column: x => x.direccion_destino_id,
                        principalTable: "direcciones_guia_snapshot",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_guias_direcciones_guia_snapshot_direccion_origen_id",
                        column: x => x.direccion_origen_id,
                        principalTable: "direcciones_guia_snapshot",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_guias_sucursales_sucursal_destino_id",
                        column: x => x.sucursal_destino_id,
                        principalTable: "sucursales",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_guias_sucursales_sucursal_origen_id",
                        column: x => x.sucursal_origen_id,
                        principalTable: "sucursales",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_guias_usuarios_usuario_alta_id",
                        column: x => x.usuario_alta_id,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_guias_usuarios_usuario_cobro_id",
                        column: x => x.usuario_cobro_id,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "articulos_guia",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    guia_id = table.Column<Guid>(type: "uuid", nullable: false),
                    articulo_id = table.Column<Guid>(type: "uuid", nullable: false),
                    cantidad = table.Column<int>(type: "integer", nullable: false),
                    peso = table.Column<decimal>(type: "numeric(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_articulos_guia", x => x.id);
                    table.ForeignKey(
                        name: "FK_articulos_guia_articulos_articulo_id",
                        column: x => x.articulo_id,
                        principalTable: "articulos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_articulos_guia_guias_guia_id",
                        column: x => x.guia_id,
                        principalTable: "guias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "asignaciones_guias",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    asignacion_id = table.Column<Guid>(type: "uuid", nullable: false),
                    guia_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_asignaciones_guias", x => x.id);
                    table.ForeignKey(
                        name: "FK_asignaciones_guias_asignaciones_asignacion_id",
                        column: x => x.asignacion_id,
                        principalTable: "asignaciones",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_asignaciones_guias_guias_guia_id",
                        column: x => x.guia_id,
                        principalTable: "guias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_articulos_guia_articulo_id",
                table: "articulos_guia",
                column: "articulo_id");

            migrationBuilder.CreateIndex(
                name: "IX_articulos_guia_guia_id",
                table: "articulos_guia",
                column: "guia_id");

            migrationBuilder.CreateIndex(
                name: "IX_asignaciones_IdChofer",
                table: "asignaciones",
                column: "IdChofer");

            migrationBuilder.CreateIndex(
                name: "IX_asignaciones_guias_asignacion_id",
                table: "asignaciones_guias",
                column: "asignacion_id");

            migrationBuilder.CreateIndex(
                name: "IX_asignaciones_guias_guia_id",
                table: "asignaciones_guias",
                column: "guia_id");

            migrationBuilder.CreateIndex(
                name: "IX_bitacora_accesos_UsuarioId",
                table: "bitacora_accesos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_bitacora_sistema_sucursal_id",
                table: "bitacora_sistema",
                column: "sucursal_id");

            migrationBuilder.CreateIndex(
                name: "IX_bitacora_sistema_usuario_id",
                table: "bitacora_sistema",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "IX_choferes_id_municipio",
                table: "choferes",
                column: "id_municipio");

            migrationBuilder.CreateIndex(
                name: "IX_clientes_municipio_id",
                table: "clientes",
                column: "municipio_id");

            migrationBuilder.CreateIndex(
                name: "IX_clientes_sucursal_id",
                table: "clientes",
                column: "sucursal_id");

            migrationBuilder.CreateIndex(
                name: "IX_direcciones_guia_snapshot_IdMunicipio",
                table: "direcciones_guia_snapshot",
                column: "IdMunicipio");

            migrationBuilder.CreateIndex(
                name: "IX_empresas_municipio_id",
                table: "empresas",
                column: "municipio_id");

            migrationBuilder.CreateIndex(
                name: "IX_guias_cliente_destino_id",
                table: "guias",
                column: "cliente_destino_id");

            migrationBuilder.CreateIndex(
                name: "IX_guias_cliente_origen_id",
                table: "guias",
                column: "cliente_origen_id");

            migrationBuilder.CreateIndex(
                name: "IX_guias_direccion_destino_id",
                table: "guias",
                column: "direccion_destino_id");

            migrationBuilder.CreateIndex(
                name: "IX_guias_direccion_origen_id",
                table: "guias",
                column: "direccion_origen_id");

            migrationBuilder.CreateIndex(
                name: "IX_guias_sucursal_destino_id",
                table: "guias",
                column: "sucursal_destino_id");

            migrationBuilder.CreateIndex(
                name: "IX_guias_sucursal_origen_id",
                table: "guias",
                column: "sucursal_origen_id");

            migrationBuilder.CreateIndex(
                name: "IX_guias_usuario_alta_id",
                table: "guias",
                column: "usuario_alta_id");

            migrationBuilder.CreateIndex(
                name: "IX_guias_usuario_cobro_id",
                table: "guias",
                column: "usuario_cobro_id");

            migrationBuilder.CreateIndex(
                name: "IX_municipios_estado",
                table: "municipios",
                column: "estado");

            migrationBuilder.CreateIndex(
                name: "IX_rutas_sucursal_destino_id",
                table: "rutas",
                column: "sucursal_destino_id");

            migrationBuilder.CreateIndex(
                name: "IX_rutas_sucursal_origen_id",
                table: "rutas",
                column: "sucursal_origen_id");

            migrationBuilder.CreateIndex(
                name: "IX_sucursales_municipio_id",
                table: "sucursales",
                column: "municipio_id");
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
