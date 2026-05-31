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
            migrationBuilder.EnsureSchema(
                name: "sat");

            migrationBuilder.EnsureSchema(
                name: "remisiones");

            migrationBuilder.EnsureSchema(
                name: "catalogos");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "articulos",
                schema: "sat",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    texto = table.Column<string>(type: "text", nullable: false),
                    similares = table.Column<string>(type: "text", nullable: false),
                    material_peligroso = table.Column<string>(type: "text", nullable: false),
                    vigencia_desde = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    vigencia_hasta = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    clave = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_articulos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "estados",
                schema: "catalogos",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    pais = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    acronimo_2 = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_estados", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "seguros",
                schema: "remisiones",
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
                name: "municipios",
                schema: "catalogos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    sat_id = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    estado = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    nombre = table.Column<string>(type: "character varying(70)", maxLength: 70, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_municipios", x => x.id);
                    table.ForeignKey(
                        name: "FK_municipios_estados_estado",
                        column: x => x.estado,
                        principalSchema: "catalogos",
                        principalTable: "estados",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "direcciones_guia_snapshot",
                schema: "remisiones",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    calle = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    numero_exterior = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    numero_interior = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    colonia = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    codigo_postal = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    localidad = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    municipio_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_direcciones_guia_snapshot", x => x.id);
                    table.ForeignKey(
                        name: "FK_direcciones_guia_snapshot_municipios_municipio_id",
                        column: x => x.municipio_id,
                        principalSchema: "catalogos",
                        principalTable: "municipios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "empresas",
                schema: "remisiones",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    nombre_corto = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    rfc = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    calle = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    numero_exterior = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    numero_interior = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    colonia = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    codigo_postal = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    localidad = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    municipio_id = table.Column<Guid>(type: "uuid", nullable: false),
                    fecha_alta = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_empresas", x => x.id);
                    table.ForeignKey(
                        name: "FK_empresas_municipios_municipio_id",
                        column: x => x.municipio_id,
                        principalSchema: "catalogos",
                        principalTable: "municipios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "choferes",
                schema: "remisiones",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    apellido_paterno = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    apellido_materno = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    estatus = table.Column<int>(type: "integer", nullable: false),
                    calle = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    numero_exterior = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    numero_interior = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    colonia = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    codigo_postal = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: true),
                    localidad = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    municipio_id = table.Column<Guid>(type: "uuid", nullable: true),
                    telefono = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    num_camion = table.Column<string>(type: "text", nullable: true),
                    num_contenedor = table.Column<string>(type: "text", nullable: true),
                    num_contenedor2 = table.Column<string>(type: "text", nullable: true),
                    fecha_alta = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fecha_baja = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    empresa_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_choferes", x => x.id);
                    table.ForeignKey(
                        name: "FK_choferes_empresas_empresa_id",
                        column: x => x.empresa_id,
                        principalSchema: "remisiones",
                        principalTable: "empresas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_choferes_municipios_municipio_id",
                        column: x => x.municipio_id,
                        principalSchema: "catalogos",
                        principalTable: "municipios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "clientes",
                schema: "remisiones",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    estatus = table.Column<int>(type: "integer", nullable: false),
                    rfc = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    telefono = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    telefono_2 = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    correo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    contacto = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    num_convenio = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    poliza_seguro = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    fecha_alta = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    empresa_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clientes", x => x.id);
                    table.ForeignKey(
                        name: "FK_clientes_empresas_empresa_id",
                        column: x => x.empresa_id,
                        principalSchema: "remisiones",
                        principalTable: "empresas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "permisos",
                schema: "remisiones",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre = table.Column<string>(type: "text", nullable: false),
                    descripcion = table.Column<string>(type: "text", nullable: false),
                    empresa_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permisos", x => x.id);
                    table.ForeignKey(
                        name: "FK_permisos_empresas_empresa_id",
                        column: x => x.empresa_id,
                        principalSchema: "remisiones",
                        principalTable: "empresas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                schema: "remisiones",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre = table.Column<string>(type: "text", nullable: false),
                    descripcion = table.Column<string>(type: "text", nullable: false),
                    empresa_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.id);
                    table.ForeignKey(
                        name: "FK_roles_empresas_empresa_id",
                        column: x => x.empresa_id,
                        principalSchema: "remisiones",
                        principalTable: "empresas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "sucursales",
                schema: "remisiones",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    codigo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    es_matriz = table.Column<bool>(type: "boolean", nullable: false),
                    calle = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    numero_exterior = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    numero_interior = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    colonia = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    codigo_postal = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    localidad = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    municipio_id = table.Column<Guid>(type: "uuid", nullable: false),
                    telefono = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    estatus = table.Column<int>(type: "integer", nullable: false),
                    servidor_ip = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    empresa_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sucursales", x => x.id);
                    table.ForeignKey(
                        name: "FK_sucursales_empresas_empresa_id",
                        column: x => x.empresa_id,
                        principalSchema: "remisiones",
                        principalTable: "empresas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_sucursales_municipios_municipio_id",
                        column: x => x.municipio_id,
                        principalSchema: "catalogos",
                        principalTable: "municipios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                schema: "remisiones",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    usuario = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    estatus = table.Column<int>(type: "integer", nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fecha_ultimo_acceso = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    empresa_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.id);
                    table.ForeignKey(
                        name: "FK_usuarios_empresas_empresa_id",
                        column: x => x.empresa_id,
                        principalSchema: "remisiones",
                        principalTable: "empresas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "asignaciones",
                schema: "remisiones",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    chofer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fecha_partida = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    st1 = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    st2 = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    st3 = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    st4 = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    camion = table.Column<string>(type: "text", nullable: false),
                    num_contenedor = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    num_contenedor2 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_asignaciones", x => x.id);
                    table.ForeignKey(
                        name: "FK_asignaciones_choferes_chofer_id",
                        column: x => x.chofer_id,
                        principalSchema: "remisiones",
                        principalTable: "choferes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "direcciones_cliente",
                schema: "remisiones",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    calle = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    numero_exterior = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    numero_interior = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    colonia = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    codigo_postal = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    localidad = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    municipio_id = table.Column<Guid>(type: "uuid", nullable: false),
                    cliente_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_direcciones_cliente", x => x.id);
                    table.ForeignKey(
                        name: "FK_direcciones_cliente_clientes_cliente_id",
                        column: x => x.cliente_id,
                        principalSchema: "remisiones",
                        principalTable: "clientes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_direcciones_cliente_municipios_municipio_id",
                        column: x => x.municipio_id,
                        principalSchema: "catalogos",
                        principalTable: "municipios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "roles_permisos",
                schema: "remisiones",
                columns: table => new
                {
                    rol_id = table.Column<Guid>(type: "uuid", nullable: false),
                    permiso_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles_permisos", x => new { x.rol_id, x.permiso_id });
                    table.ForeignKey(
                        name: "FK_roles_permisos_permisos_permiso_id",
                        column: x => x.permiso_id,
                        principalSchema: "remisiones",
                        principalTable: "permisos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_roles_permisos_roles_rol_id",
                        column: x => x.rol_id,
                        principalSchema: "remisiones",
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "rutas",
                schema: "remisiones",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    descripcion = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    sucursal_origen_id = table.Column<Guid>(type: "uuid", nullable: false),
                    sucursal_destino_id = table.Column<Guid>(type: "uuid", nullable: false),
                    empresa_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rutas", x => x.id);
                    table.ForeignKey(
                        name: "FK_rutas_empresas_empresa_id",
                        column: x => x.empresa_id,
                        principalSchema: "remisiones",
                        principalTable: "empresas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_rutas_sucursales_sucursal_destino_id",
                        column: x => x.sucursal_destino_id,
                        principalSchema: "remisiones",
                        principalTable: "sucursales",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_rutas_sucursales_sucursal_origen_id",
                        column: x => x.sucursal_origen_id,
                        principalSchema: "remisiones",
                        principalTable: "sucursales",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "bitacora_accesos",
                schema: "remisiones",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    usuario_id = table.Column<Guid>(type: "uuid", nullable: false),
                    exito = table.Column<bool>(type: "boolean", nullable: false),
                    fecha_acceso = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    cliente_ip = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bitacora_accesos", x => x.id);
                    table.ForeignKey(
                        name: "FK_bitacora_accesos_usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalSchema: "remisiones",
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "bitacora_sistema",
                schema: "remisiones",
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
                        principalSchema: "remisiones",
                        principalTable: "sucursales",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_bitacora_sistema_usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalSchema: "remisiones",
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "usuarios_roles",
                schema: "remisiones",
                columns: table => new
                {
                    usuario_id = table.Column<Guid>(type: "uuid", nullable: false),
                    rol_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios_roles", x => new { x.usuario_id, x.rol_id });
                    table.ForeignKey(
                        name: "FK_usuarios_roles_roles_rol_id",
                        column: x => x.rol_id,
                        principalSchema: "remisiones",
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_usuarios_roles_usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalSchema: "remisiones",
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "guias",
                schema: "remisiones",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    clave = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    estatus = table.Column<int>(type: "integer", nullable: false),
                    forma_pago = table.Column<int>(type: "integer", nullable: false),
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
                    costo_flete = table.Column<decimal>(type: "numeric(15,2)", nullable: false),
                    iva = table.Column<decimal>(type: "numeric(15,2)", nullable: false),
                    ivar = table.Column<decimal>(type: "numeric(15,2)", nullable: false),
                    subtotal = table.Column<decimal>(type: "numeric(15,2)", nullable: false),
                    total = table.Column<decimal>(type: "numeric(15,2)", nullable: false),
                    cobro_seguro = table.Column<decimal>(type: "numeric(15,2)", nullable: false),
                    importe_texto = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    observaciones = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    poliza_seguro = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    seguro_id = table.Column<Guid>(type: "uuid", nullable: true),
                    asignacion_id = table.Column<Guid>(type: "uuid", nullable: true),
                    empresa_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_guias", x => x.id);
                    table.ForeignKey(
                        name: "FK_guias_asignaciones_asignacion_id",
                        column: x => x.asignacion_id,
                        principalSchema: "remisiones",
                        principalTable: "asignaciones",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_guias_clientes_cliente_destino_id",
                        column: x => x.cliente_destino_id,
                        principalSchema: "remisiones",
                        principalTable: "clientes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_guias_clientes_cliente_origen_id",
                        column: x => x.cliente_origen_id,
                        principalSchema: "remisiones",
                        principalTable: "clientes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_guias_direcciones_guia_snapshot_direccion_destino_id",
                        column: x => x.direccion_destino_id,
                        principalSchema: "remisiones",
                        principalTable: "direcciones_guia_snapshot",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_guias_direcciones_guia_snapshot_direccion_origen_id",
                        column: x => x.direccion_origen_id,
                        principalSchema: "remisiones",
                        principalTable: "direcciones_guia_snapshot",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_guias_empresas_empresa_id",
                        column: x => x.empresa_id,
                        principalSchema: "remisiones",
                        principalTable: "empresas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_guias_seguros_seguro_id",
                        column: x => x.seguro_id,
                        principalSchema: "remisiones",
                        principalTable: "seguros",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_guias_sucursales_sucursal_destino_id",
                        column: x => x.sucursal_destino_id,
                        principalSchema: "remisiones",
                        principalTable: "sucursales",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_guias_sucursales_sucursal_origen_id",
                        column: x => x.sucursal_origen_id,
                        principalSchema: "remisiones",
                        principalTable: "sucursales",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_guias_usuarios_usuario_alta_id",
                        column: x => x.usuario_alta_id,
                        principalSchema: "remisiones",
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_guias_usuarios_usuario_cobro_id",
                        column: x => x.usuario_cobro_id,
                        principalSchema: "remisiones",
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "articulos_guia",
                schema: "remisiones",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    descipcion = table.Column<string>(type: "text", nullable: false),
                    guia_id = table.Column<Guid>(type: "uuid", nullable: false),
                    articulo_id = table.Column<string>(type: "text", nullable: false),
                    cantidad = table.Column<int>(type: "integer", nullable: false),
                    peso_unidad = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    valor_unidad = table.Column<decimal>(type: "numeric(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_articulos_guia", x => x.id);
                    table.ForeignKey(
                        name: "FK_articulos_guia_articulos_articulo_id",
                        column: x => x.articulo_id,
                        principalSchema: "sat",
                        principalTable: "articulos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_articulos_guia_guias_guia_id",
                        column: x => x.guia_id,
                        principalSchema: "remisiones",
                        principalTable: "guias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_articulos_guia_articulo_id",
                schema: "remisiones",
                table: "articulos_guia",
                column: "articulo_id");

            migrationBuilder.CreateIndex(
                name: "IX_articulos_guia_guia_id",
                schema: "remisiones",
                table: "articulos_guia",
                column: "guia_id");

            migrationBuilder.CreateIndex(
                name: "IX_asignaciones_chofer_id",
                schema: "remisiones",
                table: "asignaciones",
                column: "chofer_id");

            migrationBuilder.CreateIndex(
                name: "IX_bitacora_accesos_UsuarioId",
                schema: "remisiones",
                table: "bitacora_accesos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_bitacora_sistema_sucursal_id",
                schema: "remisiones",
                table: "bitacora_sistema",
                column: "sucursal_id");

            migrationBuilder.CreateIndex(
                name: "IX_bitacora_sistema_usuario_id",
                schema: "remisiones",
                table: "bitacora_sistema",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "IX_choferes_empresa_id",
                schema: "remisiones",
                table: "choferes",
                column: "empresa_id");

            migrationBuilder.CreateIndex(
                name: "IX_choferes_municipio_id",
                schema: "remisiones",
                table: "choferes",
                column: "municipio_id");

            migrationBuilder.CreateIndex(
                name: "IX_clientes_empresa_id",
                schema: "remisiones",
                table: "clientes",
                column: "empresa_id");

            migrationBuilder.CreateIndex(
                name: "IX_direcciones_cliente_cliente_id",
                schema: "remisiones",
                table: "direcciones_cliente",
                column: "cliente_id");

            migrationBuilder.CreateIndex(
                name: "IX_direcciones_cliente_municipio_id",
                schema: "remisiones",
                table: "direcciones_cliente",
                column: "municipio_id");

            migrationBuilder.CreateIndex(
                name: "IX_direcciones_guia_snapshot_municipio_id",
                schema: "remisiones",
                table: "direcciones_guia_snapshot",
                column: "municipio_id");

            migrationBuilder.CreateIndex(
                name: "IX_empresas_municipio_id",
                schema: "remisiones",
                table: "empresas",
                column: "municipio_id");

            migrationBuilder.CreateIndex(
                name: "IX_guias_asignacion_id",
                schema: "remisiones",
                table: "guias",
                column: "asignacion_id");

            migrationBuilder.CreateIndex(
                name: "IX_guias_cliente_destino_id",
                schema: "remisiones",
                table: "guias",
                column: "cliente_destino_id");

            migrationBuilder.CreateIndex(
                name: "IX_guias_cliente_origen_id",
                schema: "remisiones",
                table: "guias",
                column: "cliente_origen_id");

            migrationBuilder.CreateIndex(
                name: "IX_guias_direccion_destino_id",
                schema: "remisiones",
                table: "guias",
                column: "direccion_destino_id");

            migrationBuilder.CreateIndex(
                name: "IX_guias_direccion_origen_id",
                schema: "remisiones",
                table: "guias",
                column: "direccion_origen_id");

            migrationBuilder.CreateIndex(
                name: "IX_guias_empresa_id",
                schema: "remisiones",
                table: "guias",
                column: "empresa_id");

            migrationBuilder.CreateIndex(
                name: "IX_guias_seguro_id",
                schema: "remisiones",
                table: "guias",
                column: "seguro_id");

            migrationBuilder.CreateIndex(
                name: "IX_guias_sucursal_destino_id",
                schema: "remisiones",
                table: "guias",
                column: "sucursal_destino_id");

            migrationBuilder.CreateIndex(
                name: "IX_guias_sucursal_origen_id",
                schema: "remisiones",
                table: "guias",
                column: "sucursal_origen_id");

            migrationBuilder.CreateIndex(
                name: "IX_guias_usuario_alta_id",
                schema: "remisiones",
                table: "guias",
                column: "usuario_alta_id");

            migrationBuilder.CreateIndex(
                name: "IX_guias_usuario_cobro_id",
                schema: "remisiones",
                table: "guias",
                column: "usuario_cobro_id");

            migrationBuilder.CreateIndex(
                name: "IX_municipios_estado",
                schema: "catalogos",
                table: "municipios",
                column: "estado");

            migrationBuilder.CreateIndex(
                name: "IX_permisos_empresa_id",
                schema: "remisiones",
                table: "permisos",
                column: "empresa_id");

            migrationBuilder.CreateIndex(
                name: "IX_roles_empresa_id",
                schema: "remisiones",
                table: "roles",
                column: "empresa_id");

            migrationBuilder.CreateIndex(
                name: "IX_roles_permisos_permiso_id",
                schema: "remisiones",
                table: "roles_permisos",
                column: "permiso_id");

            migrationBuilder.CreateIndex(
                name: "IX_rutas_empresa_id",
                schema: "remisiones",
                table: "rutas",
                column: "empresa_id");

            migrationBuilder.CreateIndex(
                name: "IX_rutas_sucursal_destino_id",
                schema: "remisiones",
                table: "rutas",
                column: "sucursal_destino_id");

            migrationBuilder.CreateIndex(
                name: "IX_rutas_sucursal_origen_id",
                schema: "remisiones",
                table: "rutas",
                column: "sucursal_origen_id");

            migrationBuilder.CreateIndex(
                name: "IX_sucursales_empresa_id",
                schema: "remisiones",
                table: "sucursales",
                column: "empresa_id");

            migrationBuilder.CreateIndex(
                name: "IX_sucursales_municipio_id",
                schema: "remisiones",
                table: "sucursales",
                column: "municipio_id");

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_empresa_id",
                schema: "remisiones",
                table: "usuarios",
                column: "empresa_id");

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_roles_rol_id",
                schema: "remisiones",
                table: "usuarios_roles",
                column: "rol_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "articulos_guia",
                schema: "remisiones");

            migrationBuilder.DropTable(
                name: "bitacora_accesos",
                schema: "remisiones");

            migrationBuilder.DropTable(
                name: "bitacora_sistema",
                schema: "remisiones");

            migrationBuilder.DropTable(
                name: "direcciones_cliente",
                schema: "remisiones");

            migrationBuilder.DropTable(
                name: "roles_permisos",
                schema: "remisiones");

            migrationBuilder.DropTable(
                name: "rutas",
                schema: "remisiones");

            migrationBuilder.DropTable(
                name: "usuarios_roles",
                schema: "remisiones");

            migrationBuilder.DropTable(
                name: "articulos",
                schema: "sat");

            migrationBuilder.DropTable(
                name: "guias",
                schema: "remisiones");

            migrationBuilder.DropTable(
                name: "permisos",
                schema: "remisiones");

            migrationBuilder.DropTable(
                name: "roles",
                schema: "remisiones");

            migrationBuilder.DropTable(
                name: "asignaciones",
                schema: "remisiones");

            migrationBuilder.DropTable(
                name: "clientes",
                schema: "remisiones");

            migrationBuilder.DropTable(
                name: "direcciones_guia_snapshot",
                schema: "remisiones");

            migrationBuilder.DropTable(
                name: "seguros",
                schema: "remisiones");

            migrationBuilder.DropTable(
                name: "sucursales",
                schema: "remisiones");

            migrationBuilder.DropTable(
                name: "usuarios",
                schema: "remisiones");

            migrationBuilder.DropTable(
                name: "choferes",
                schema: "remisiones");

            migrationBuilder.DropTable(
                name: "empresas",
                schema: "remisiones");

            migrationBuilder.DropTable(
                name: "municipios",
                schema: "catalogos");

            migrationBuilder.DropTable(
                name: "estados",
                schema: "catalogos");
        }
    }
}
