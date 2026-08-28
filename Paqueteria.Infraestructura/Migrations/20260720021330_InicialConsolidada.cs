using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Paqueteria.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InicialConsolidada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "sys");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "articulos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    clave = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    texto = table.Column<string>(type: "text", nullable: false),
                    similares = table.Column<string>(type: "text", nullable: false),
                    material_peligroso = table.Column<string>(type: "text", nullable: false),
                    vigencia_desde = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    vigencia_hasta = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_articulos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "estados",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    nombre = table.Column<string>(type: "text", nullable: false),
                    pais = table.Column<string>(type: "text", nullable: false),
                    acronimo = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_estados", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "municipios",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    sat_id = table.Column<string>(type: "text", nullable: false),
                    nombre = table.Column<string>(type: "character varying(70)", maxLength: 70, nullable: false),
                    estado_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_municipios", x => x.id);
                    table.ForeignKey(
                        name: "FK_municipios_estados_estado_id",
                        column: x => x.estado_id,
                        principalTable: "estados",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "direcciones_guia_snapshot",
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
                    calle = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    numero_exterior = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    numero_interior = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    colonia = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    codigo_postal = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: true),
                    localidad = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    municipio_id = table.Column<Guid>(type: "uuid", nullable: true),
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
                name: "camiones",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    num_camion = table.Column<string>(type: "text", nullable: true),
                    placa = table.Column<string>(type: "text", nullable: true),
                    empresa_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_camiones", x => x.id);
                    table.ForeignKey(
                        name: "FK_camiones_empresas_empresa_id",
                        column: x => x.empresa_id,
                        principalTable: "empresas",
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
                        principalTable: "empresas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "permisos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    descripcion = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    estatus = table.Column<int>(type: "integer", nullable: false),
                    empresa_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permisos", x => x.id);
                    table.ForeignKey(
                        name: "FK_permisos_empresas_empresa_id",
                        column: x => x.empresa_id,
                        principalTable: "empresas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre = table.Column<string>(type: "text", nullable: false),
                    descripcion = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    estatus = table.Column<int>(type: "integer", nullable: false),
                    empresa_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.id);
                    table.ForeignKey(
                        name: "FK_roles_empresas_empresa_id",
                        column: x => x.empresa_id,
                        principalTable: "empresas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "seguros",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre = table.Column<string>(type: "text", nullable: false),
                    estatus = table.Column<int>(type: "integer", nullable: false),
                    empresa_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_seguros", x => x.id);
                    table.ForeignKey(
                        name: "FK_seguros_empresas_empresa_id",
                        column: x => x.empresa_id,
                        principalTable: "empresas",
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
                        principalTable: "empresas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_sucursales_municipios_municipio_id",
                        column: x => x.municipio_id,
                        principalTable: "municipios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
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
                        principalTable: "empresas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "choferes",
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
                    telefono = table.Column<string>(type: "text", nullable: true),
                    NumCamion = table.Column<string>(type: "text", nullable: true),
                    NumContenedor = table.Column<string>(type: "text", nullable: true),
                    NumContenedor2 = table.Column<string>(type: "text", nullable: true),
                    camion_id = table.Column<Guid>(type: "uuid", nullable: true),
                    fecha_alta = table.Column<DateTime>(type: "date", nullable: false),
                    fecha_baja = table.Column<DateTime>(type: "date", nullable: true),
                    empresa_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_choferes", x => x.id);
                    table.ForeignKey(
                        name: "FK_choferes_camiones_camion_id",
                        column: x => x.camion_id,
                        principalTable: "camiones",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_choferes_empresas_empresa_id",
                        column: x => x.empresa_id,
                        principalTable: "empresas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_choferes_municipios_municipio_id",
                        column: x => x.municipio_id,
                        principalTable: "municipios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "direcciones_clientes",
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
                    estatus = table.Column<int>(type: "integer", nullable: false),
                    cliente_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_direcciones_clientes", x => x.id);
                    table.ForeignKey(
                        name: "FK_direcciones_clientes_clientes_cliente_id",
                        column: x => x.cliente_id,
                        principalTable: "clientes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_direcciones_clientes_municipios_municipio_id",
                        column: x => x.municipio_id,
                        principalTable: "municipios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "roles_permisos",
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
                        principalTable: "permisos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_roles_permisos_roles_rol_id",
                        column: x => x.rol_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "folios_sucursales",
                columns: table => new
                {
                    sucursal_id = table.Column<Guid>(type: "uuid", nullable: false),
                    ultimo_consecutivo = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_folios_sucursales", x => x.sucursal_id);
                    table.ForeignKey(
                        name: "FK_folios_sucursales_sucursales_sucursal_id",
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
                    descripcion = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    sucursal_origen_id = table.Column<Guid>(type: "uuid", nullable: false),
                    sucursal_destino_id = table.Column<Guid>(type: "uuid", nullable: false),
                    estatus = table.Column<int>(type: "integer", nullable: false),
                    empresa_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rutas", x => x.id);
                    table.ForeignKey(
                        name: "FK_rutas_empresas_empresa_id",
                        column: x => x.empresa_id,
                        principalTable: "empresas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "bitacora_accesos",
                schema: "sys",
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
                        principalTable: "usuarios",
                        principalColumn: "id",
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
                name: "usuarios_roles",
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
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_usuarios_roles_usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "asignaciones",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fecha_partida = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    st1 = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    st2 = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    st3 = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    st4 = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    chofer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    empresa_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_asignaciones", x => x.id);
                    table.ForeignKey(
                        name: "FK_asignaciones_choferes_chofer_id",
                        column: x => x.chofer_id,
                        principalTable: "choferes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_asignaciones_empresas_empresa_id",
                        column: x => x.empresa_id,
                        principalTable: "empresas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "guias",
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
                    costo_flete = table.Column<decimal>(type: "numeric(15,2)", precision: 15, scale: 2, nullable: false),
                    iva = table.Column<decimal>(type: "numeric(15,2)", precision: 15, scale: 2, nullable: false),
                    ivar = table.Column<decimal>(type: "numeric(15,2)", precision: 15, scale: 2, nullable: false),
                    subtotal = table.Column<decimal>(type: "numeric(15,2)", precision: 15, scale: 2, nullable: false),
                    total = table.Column<decimal>(type: "numeric(15,2)", precision: 15, scale: 2, nullable: false),
                    cobro_seguro = table.Column<decimal>(type: "numeric(15,2)", precision: 15, scale: 2, nullable: false),
                    importe_texto = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    observaciones = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    poliza_seguro = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    seguro_id = table.Column<Guid>(type: "uuid", nullable: true),
                    asignacion_id = table.Column<Guid>(type: "uuid", nullable: true),
                    empresa_id = table.Column<Guid>(type: "uuid", nullable: false),
                    AsignacionId1 = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_guias", x => x.id);
                    table.ForeignKey(
                        name: "FK_guias_asignaciones_AsignacionId1",
                        column: x => x.AsignacionId1,
                        principalTable: "asignaciones",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_guias_asignaciones_asignacion_id",
                        column: x => x.asignacion_id,
                        principalTable: "asignaciones",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
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
                        name: "FK_guias_empresas_empresa_id",
                        column: x => x.empresa_id,
                        principalTable: "empresas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_guias_seguros_seguro_id",
                        column: x => x.seguro_id,
                        principalTable: "seguros",
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
                    descripcion = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    guia_id = table.Column<Guid>(type: "uuid", nullable: false),
                    articulo_id = table.Column<Guid>(type: "uuid", nullable: false),
                    cantidad = table.Column<int>(type: "integer", precision: 10, nullable: false),
                    peso_unidad = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    valor_unidad = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    guia_id1 = table.Column<Guid>(type: "uuid", nullable: false)
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
                    table.ForeignKey(
                        name: "FK_articulos_guia_guias_guia_id1",
                        column: x => x.guia_id1,
                        principalTable: "guias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_articulos_clave",
                table: "articulos",
                column: "clave");

            migrationBuilder.CreateIndex(
                name: "IX_articulos_guia_articulo_id",
                table: "articulos_guia",
                column: "articulo_id");

            migrationBuilder.CreateIndex(
                name: "IX_articulos_guia_guia_id",
                table: "articulos_guia",
                column: "guia_id");

            migrationBuilder.CreateIndex(
                name: "IX_articulos_guia_guia_id1",
                table: "articulos_guia",
                column: "guia_id1");

            migrationBuilder.CreateIndex(
                name: "IX_asignaciones_chofer_id",
                table: "asignaciones",
                column: "chofer_id");

            migrationBuilder.CreateIndex(
                name: "IX_asignaciones_empresa_id",
                table: "asignaciones",
                column: "empresa_id");

            migrationBuilder.CreateIndex(
                name: "IX_bitacora_accesos_UsuarioId",
                schema: "sys",
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
                name: "IX_camiones_empresa_id",
                table: "camiones",
                column: "empresa_id");

            migrationBuilder.CreateIndex(
                name: "IX_camiones_placa",
                table: "camiones",
                column: "placa",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_choferes_camion_id",
                table: "choferes",
                column: "camion_id");

            migrationBuilder.CreateIndex(
                name: "IX_choferes_empresa_id",
                table: "choferes",
                column: "empresa_id");

            migrationBuilder.CreateIndex(
                name: "IX_choferes_municipio_id",
                table: "choferes",
                column: "municipio_id");

            migrationBuilder.CreateIndex(
                name: "IX_clientes_empresa_id",
                table: "clientes",
                column: "empresa_id");

            migrationBuilder.CreateIndex(
                name: "IX_clientes_rfc",
                table: "clientes",
                column: "rfc",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_direcciones_clientes_cliente_id",
                table: "direcciones_clientes",
                column: "cliente_id");

            migrationBuilder.CreateIndex(
                name: "IX_direcciones_clientes_municipio_id",
                table: "direcciones_clientes",
                column: "municipio_id");

            migrationBuilder.CreateIndex(
                name: "IX_direcciones_guia_snapshot_municipio_id",
                table: "direcciones_guia_snapshot",
                column: "municipio_id");

            migrationBuilder.CreateIndex(
                name: "IX_empresas_municipio_id",
                table: "empresas",
                column: "municipio_id");

            migrationBuilder.CreateIndex(
                name: "IX_empresas_rfc",
                table: "empresas",
                column: "rfc",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_guias_asignacion_id",
                table: "guias",
                column: "asignacion_id");

            migrationBuilder.CreateIndex(
                name: "IX_guias_AsignacionId1",
                table: "guias",
                column: "AsignacionId1");

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
                name: "IX_guias_empresa_id",
                table: "guias",
                column: "empresa_id");

            migrationBuilder.CreateIndex(
                name: "IX_guias_seguro_id",
                table: "guias",
                column: "seguro_id");

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
                name: "IX_municipios_estado_id",
                table: "municipios",
                column: "estado_id");

            migrationBuilder.CreateIndex(
                name: "IX_permisos_empresa_id",
                table: "permisos",
                column: "empresa_id");

            migrationBuilder.CreateIndex(
                name: "IX_roles_empresa_id",
                table: "roles",
                column: "empresa_id");

            migrationBuilder.CreateIndex(
                name: "IX_roles_permisos_permiso_id",
                table: "roles_permisos",
                column: "permiso_id");

            migrationBuilder.CreateIndex(
                name: "IX_rutas_empresa_id",
                table: "rutas",
                column: "empresa_id");

            migrationBuilder.CreateIndex(
                name: "IX_rutas_sucursal_destino_id",
                table: "rutas",
                column: "sucursal_destino_id");

            migrationBuilder.CreateIndex(
                name: "IX_rutas_sucursal_origen_id",
                table: "rutas",
                column: "sucursal_origen_id");

            migrationBuilder.CreateIndex(
                name: "IX_seguros_empresa_id",
                table: "seguros",
                column: "empresa_id");

            migrationBuilder.CreateIndex(
                name: "IX_sucursales_empresa_id",
                table: "sucursales",
                column: "empresa_id");

            migrationBuilder.CreateIndex(
                name: "IX_sucursales_municipio_id",
                table: "sucursales",
                column: "municipio_id");

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_empresa_id",
                table: "usuarios",
                column: "empresa_id");

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_usuario",
                table: "usuarios",
                column: "usuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_roles_rol_id",
                table: "usuarios_roles",
                column: "rol_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "articulos_guia");

            migrationBuilder.DropTable(
                name: "bitacora_accesos",
                schema: "sys");

            migrationBuilder.DropTable(
                name: "bitacora_sistema");

            migrationBuilder.DropTable(
                name: "direcciones_clientes");

            migrationBuilder.DropTable(
                name: "folios_sucursales");

            migrationBuilder.DropTable(
                name: "roles_permisos");

            migrationBuilder.DropTable(
                name: "rutas");

            migrationBuilder.DropTable(
                name: "usuarios_roles");

            migrationBuilder.DropTable(
                name: "articulos");

            migrationBuilder.DropTable(
                name: "guias");

            migrationBuilder.DropTable(
                name: "permisos");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "asignaciones");

            migrationBuilder.DropTable(
                name: "clientes");

            migrationBuilder.DropTable(
                name: "direcciones_guia_snapshot");

            migrationBuilder.DropTable(
                name: "seguros");

            migrationBuilder.DropTable(
                name: "sucursales");

            migrationBuilder.DropTable(
                name: "usuarios");

            migrationBuilder.DropTable(
                name: "choferes");

            migrationBuilder.DropTable(
                name: "camiones");

            migrationBuilder.DropTable(
                name: "empresas");

            migrationBuilder.DropTable(
                name: "municipios");

            migrationBuilder.DropTable(
                name: "estados");
        }
    }
}
