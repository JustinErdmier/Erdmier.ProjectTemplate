#nullable disable

namespace Erdmier.ProjectTemplate.Infrastructure.Persistence.Migrations.Application;

/// <inheritdoc />
public partial class Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterDatabase()
                        .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(name: "ApplicationRoles",
                                     columns: table => new
                                     {
                                         Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                                         Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                                                     .Annotation("MySql:CharSet", "utf8mb4"),
                                         NormalizedName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                                                               .Annotation("MySql:CharSet", "utf8mb4"),
                                         ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                                                                 .Annotation("MySql:CharSet", "utf8mb4")
                                     },
                                     constraints: table =>
                                     {
                                         table.PrimaryKey("PK_ApplicationRoles", x => x.Id);
                                     })
                        .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(name: "ApplicationUsers",
                                     columns: table => new
                                     {
                                         Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                                         UserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                                                         .Annotation("MySql:CharSet", "utf8mb4"),
                                         NormalizedUserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                                                                   .Annotation("MySql:CharSet", "utf8mb4"),
                                         Email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                                                      .Annotation("MySql:CharSet", "utf8mb4"),
                                         NormalizedEmail = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                                                                .Annotation("MySql:CharSet", "utf8mb4"),
                                         EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                                         PasswordHash = table.Column<string>(type: "longtext", nullable: true)
                                                             .Annotation("MySql:CharSet", "utf8mb4"),
                                         SecurityStamp = table.Column<string>(type: "longtext", nullable: true)
                                                              .Annotation("MySql:CharSet", "utf8mb4"),
                                         ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                                                                 .Annotation("MySql:CharSet", "utf8mb4"),
                                         PhoneNumber = table.Column<string>(type: "longtext", nullable: true)
                                                            .Annotation("MySql:CharSet", "utf8mb4"),
                                         PhoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                                         TwoFactorEnabled     = table.Column<bool>(type: "tinyint(1)", nullable: false),
                                         LockoutEnd           = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                                         LockoutEnabled       = table.Column<bool>(type: "tinyint(1)", nullable: false),
                                         AccessFailedCount    = table.Column<int>(type: "int", nullable: false)
                                     },
                                     constraints: table =>
                                     {
                                         table.PrimaryKey("PK_ApplicationUsers", x => x.Id);
                                     })
                        .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(name: "ApplicationRoleClaims",
                                     columns: table => new
                                     {
                                         Id = table.Column<int>(type: "int", nullable: false)
                                                   .Annotation("MySql:ValueGenerationStrategy",
                                                               MySqlValueGenerationStrategy.IdentityColumn),
                                         RoleId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                                         ClaimType = table.Column<string>(type: "longtext", nullable: true)
                                                          .Annotation("MySql:CharSet", "utf8mb4"),
                                         ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                                                           .Annotation("MySql:CharSet", "utf8mb4")
                                     },
                                     constraints: table =>
                                     {
                                         table.PrimaryKey("PK_ApplicationRoleClaims", x => x.Id);

                                         table.ForeignKey(name: "FK_ApplicationRoleClaims_ApplicationRoles_RoleId",
                                                          column: x => x.RoleId,
                                                          principalTable: "ApplicationRoles",
                                                          principalColumn: "Id",
                                                          onDelete: ReferentialAction.Cascade);
                                     })
                        .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(name: "ApplicationUserClaims",
                                     columns: table => new
                                     {
                                         Id = table.Column<int>(type: "int", nullable: false)
                                                   .Annotation("MySql:ValueGenerationStrategy",
                                                               MySqlValueGenerationStrategy.IdentityColumn),
                                         UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                                         ClaimType = table.Column<string>(type: "longtext", nullable: true)
                                                          .Annotation("MySql:CharSet", "utf8mb4"),
                                         ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                                                           .Annotation("MySql:CharSet", "utf8mb4")
                                     },
                                     constraints: table =>
                                     {
                                         table.PrimaryKey("PK_ApplicationUserClaims", x => x.Id);

                                         table.ForeignKey(name: "FK_ApplicationUserClaims_ApplicationUsers_UserId",
                                                          column: x => x.UserId,
                                                          principalTable: "ApplicationUsers",
                                                          principalColumn: "Id",
                                                          onDelete: ReferentialAction.Cascade);
                                     })
                        .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(name: "ApplicationUserLogins",
                                     columns: table => new
                                     {
                                         UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                                         LoginProvider = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true)
                                                              .Annotation("MySql:CharSet", "utf8mb4"),
                                         ProviderKey = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true)
                                                            .Annotation("MySql:CharSet", "utf8mb4"),
                                         ProviderDisplayName = table.Column<string>(type: "longtext", nullable: true)
                                                                    .Annotation("MySql:CharSet", "utf8mb4")
                                     },
                                     constraints: table =>
                                     {
                                         table.PrimaryKey("PK_ApplicationUserLogins", x => x.UserId);

                                         table.ForeignKey(name: "FK_ApplicationUserLogins_ApplicationUsers_UserId",
                                                          column: x => x.UserId,
                                                          principalTable: "ApplicationUsers",
                                                          principalColumn: "Id",
                                                          onDelete: ReferentialAction.Cascade);
                                     })
                        .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(name: "ApplicationUserProfileImages",
                                     columns: table => new
                                     {
                                         Id      = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                                         Content = table.Column<byte[]>(type: "longblob", maxLength: 2097152, nullable: false),
                                         UntrustedName = table.Column<string>(type: "longtext", nullable: false)
                                                              .Annotation("MySql:CharSet", "utf8mb4"),
                                         Size = table.Column<long>(type: "bigint", nullable: false),
                                         Extension = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                                                          .Annotation("MySql:CharSet", "utf8mb4"),
                                         UploadedUtc = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                                         UserId      = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                                     },
                                     constraints: table =>
                                     {
                                         table.PrimaryKey("PK_ApplicationUserProfileImages", x => x.Id);

                                         table.ForeignKey(name: "FK_ApplicationUserProfileImages_ApplicationUsers_UserId",
                                                          column: x => x.UserId,
                                                          principalTable: "ApplicationUsers",
                                                          principalColumn: "Id",
                                                          onDelete: ReferentialAction.Cascade);
                                     })
                        .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(name: "ApplicationUserRoles",
                                     columns: table => new
                                     {
                                         UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                                         RoleId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                                     },
                                     constraints: table =>
                                     {
                                         table.PrimaryKey("PK_ApplicationUserRoles", x => new { x.UserId, x.RoleId });

                                         table.ForeignKey(name: "FK_ApplicationUserRoles_ApplicationRoles_RoleId",
                                                          column: x => x.RoleId,
                                                          principalTable: "ApplicationRoles",
                                                          principalColumn: "Id",
                                                          onDelete: ReferentialAction.Cascade);

                                         table.ForeignKey(name: "FK_ApplicationUserRoles_ApplicationUsers_UserId",
                                                          column: x => x.UserId,
                                                          principalTable: "ApplicationUsers",
                                                          principalColumn: "Id",
                                                          onDelete: ReferentialAction.Cascade);
                                     })
                        .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(name: "ApplicationUserTokens",
                                     columns: table => new
                                     {
                                         UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                                         LoginProvider = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false)
                                                              .Annotation("MySql:CharSet", "utf8mb4"),
                                         Name = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false)
                                                     .Annotation("MySql:CharSet", "utf8mb4"),
                                         Value = table.Column<string>(type: "longtext", nullable: true)
                                                      .Annotation("MySql:CharSet", "utf8mb4")
                                     },
                                     constraints: table =>
                                     {
                                         table.PrimaryKey("PK_ApplicationUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });

                                         table.ForeignKey(name: "FK_ApplicationUserTokens_ApplicationUsers_UserId",
                                                          column: x => x.UserId,
                                                          principalTable: "ApplicationUsers",
                                                          principalColumn: "Id",
                                                          onDelete: ReferentialAction.Cascade);
                                     })
                        .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateIndex(name: "IX_ApplicationRoleClaims_RoleId",
                                     table: "ApplicationRoleClaims",
                                     column: "RoleId");

        migrationBuilder.CreateIndex(name: "IX_ApplicationRoles_Id",
                                     table: "ApplicationRoles",
                                     column: "Id");

        migrationBuilder.CreateIndex(name: "RoleNameIndex",
                                     table: "ApplicationRoles",
                                     column: "NormalizedName",
                                     unique: true);

        migrationBuilder.CreateIndex(name: "IX_ApplicationUserClaims_UserId",
                                     table: "ApplicationUserClaims",
                                     column: "UserId");

        migrationBuilder.CreateIndex(name: "IX_ApplicationUserProfileImages_UserId",
                                     table: "ApplicationUserProfileImages",
                                     column: "UserId",
                                     unique: true);

        migrationBuilder.CreateIndex(name: "IX_ApplicationUserRoles_RoleId",
                                     table: "ApplicationUserRoles",
                                     column: "RoleId");

        migrationBuilder.CreateIndex(name: "EmailIndex",
                                     table: "ApplicationUsers",
                                     column: "NormalizedEmail",
                                     unique: true);

        migrationBuilder.CreateIndex(name: "UserNameIndex",
                                     table: "ApplicationUsers",
                                     column: "NormalizedUserName",
                                     unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "ApplicationRoleClaims");

        migrationBuilder.DropTable(name: "ApplicationUserClaims");

        migrationBuilder.DropTable(name: "ApplicationUserLogins");

        migrationBuilder.DropTable(name: "ApplicationUserProfileImages");

        migrationBuilder.DropTable(name: "ApplicationUserRoles");

        migrationBuilder.DropTable(name: "ApplicationUserTokens");

        migrationBuilder.DropTable(name: "ApplicationRoles");

        migrationBuilder.DropTable(name: "ApplicationUsers");
    }
}
