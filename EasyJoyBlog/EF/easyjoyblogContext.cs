using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EasyJoyBlog.EF
{
    public partial class easyjoyblogContext : DbContext
    {
        public easyjoyblogContext()
        {
        }

        public easyjoyblogContext(DbContextOptions<easyjoyblogContext> options)
            : base(options)
        {
        }

        public virtual DbSet<EjbCommentmeta> EjbCommentmeta { get; set; }
        public virtual DbSet<EjbComments> EjbComments { get; set; }
        public virtual DbSet<EjbLinks> EjbLinks { get; set; }
        public virtual DbSet<EjbOptions> EjbOptions { get; set; }
        public virtual DbSet<EjbPostmeta> EjbPostmeta { get; set; }
        public virtual DbSet<EjbPosts> EjbPosts { get; set; }
        public virtual DbSet<EjbTermmeta> EjbTermmeta { get; set; }
        public virtual DbSet<EjbTermRelationships> EjbTermRelationships { get; set; }
        public virtual DbSet<EjbTerms> EjbTerms { get; set; }
        public virtual DbSet<EjbTermTaxonomy> EjbTermTaxonomy { get; set; }
        public virtual DbSet<EjbUsermeta> EjbUsermeta { get; set; }
        public virtual DbSet<EjbUsers> EjbUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("Server=******;Database=easyjoyblog;Uid=******;Pwd=******;CharSet=utf8");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EjbCommentmeta>(entity =>
            {
                entity.HasKey(e => e.MetaId);

                entity.ToTable("ejb_commentmeta");

                entity.HasIndex(e => e.CommentId)
                    .HasName("comment_id");

                entity.HasIndex(e => e.MetaKey)
                    .HasName("meta_key");

                entity.Property(e => e.MetaId).HasColumnName("meta_id");

                entity.Property(e => e.CommentId)
                    .HasColumnName("comment_id")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.MetaKey)
                    .HasColumnName("meta_key")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.MetaValue)
                    .HasColumnName("meta_value")
                    .HasColumnType("longtext");
            });

            modelBuilder.Entity<EjbComments>(entity =>
            {
                entity.HasKey(e => e.CommentId);

                entity.ToTable("ejb_comments");

                entity.HasIndex(e => e.CommentAuthorEmail)
                    .HasName("comment_author_email");

                entity.HasIndex(e => e.CommentDateGmt)
                    .HasName("comment_date_gmt");

                entity.HasIndex(e => e.CommentParent)
                    .HasName("comment_parent");

                entity.HasIndex(e => e.CommentPostId)
                    .HasName("comment_post_ID");

                entity.HasIndex(e => new { e.CommentApproved, e.CommentDateGmt })
                    .HasName("comment_approved_date_gmt");

                entity.Property(e => e.CommentId).HasColumnName("comment_ID");

                entity.Property(e => e.CommentAgent)
                    .IsRequired()
                    .HasColumnName("comment_agent")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.CommentApproved)
                    .IsRequired()
                    .HasColumnName("comment_approved")
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.CommentAuthor)
                    .IsRequired()
                    .HasColumnName("comment_author")
                    .HasColumnType("tinytext");

                entity.Property(e => e.CommentAuthorEmail)
                    .IsRequired()
                    .HasColumnName("comment_author_email")
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.CommentAuthorIp)
                    .IsRequired()
                    .HasColumnName("comment_author_IP")
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.CommentAuthorUrl)
                    .IsRequired()
                    .HasColumnName("comment_author_url")
                    .HasColumnType("varchar(200)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.CommentContent)
                    .IsRequired()
                    .HasColumnName("comment_content")
                    .HasColumnType("text");

                entity.Property(e => e.CommentDate)
                    .HasColumnName("comment_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'0000-00-00 00:00:00'");

                entity.Property(e => e.CommentDateGmt)
                    .HasColumnName("comment_date_gmt")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'0000-00-00 00:00:00'");

                entity.Property(e => e.CommentKarma)
                    .HasColumnName("comment_karma")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.CommentParent)
                    .HasColumnName("comment_parent")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.CommentPostId)
                    .HasColumnName("comment_post_ID")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.CommentType)
                    .IsRequired()
                    .HasColumnName("comment_type")
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<EjbLinks>(entity =>
            {
                entity.HasKey(e => e.LinkId);

                entity.ToTable("ejb_links");

                entity.HasIndex(e => e.LinkVisible)
                    .HasName("link_visible");

                entity.Property(e => e.LinkId).HasColumnName("link_id");

                entity.Property(e => e.LinkDescription)
                    .IsRequired()
                    .HasColumnName("link_description")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.LinkImage)
                    .IsRequired()
                    .HasColumnName("link_image")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.LinkName)
                    .IsRequired()
                    .HasColumnName("link_name")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.LinkNotes)
                    .IsRequired()
                    .HasColumnName("link_notes")
                    .HasColumnType("mediumtext");

                entity.Property(e => e.LinkOwner)
                    .HasColumnName("link_owner")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.LinkRating)
                    .HasColumnName("link_rating")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.LinkRel)
                    .IsRequired()
                    .HasColumnName("link_rel")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.LinkRss)
                    .IsRequired()
                    .HasColumnName("link_rss")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.LinkTarget)
                    .IsRequired()
                    .HasColumnName("link_target")
                    .HasColumnType("varchar(25)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.LinkUpdated)
                    .HasColumnName("link_updated")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'0000-00-00 00:00:00'");

                entity.Property(e => e.LinkUrl)
                    .IsRequired()
                    .HasColumnName("link_url")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.LinkVisible)
                    .IsRequired()
                    .HasColumnName("link_visible")
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'Y'");
            });

            modelBuilder.Entity<EjbOptions>(entity =>
            {
                entity.HasKey(e => e.OptionId);

                entity.ToTable("ejb_options");

                entity.HasIndex(e => e.OptionName)
                    .HasName("option_name")
                    .IsUnique();

                entity.Property(e => e.OptionId).HasColumnName("option_id");

                entity.Property(e => e.Autoload)
                    .IsRequired()
                    .HasColumnName("autoload")
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'yes'");

                entity.Property(e => e.OptionName)
                    .IsRequired()
                    .HasColumnName("option_name")
                    .HasColumnType("varchar(191)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.OptionValue)
                    .IsRequired()
                    .HasColumnName("option_value")
                    .HasColumnType("longtext");
            });

            modelBuilder.Entity<EjbPostmeta>(entity =>
            {
                entity.HasKey(e => e.MetaId);

                entity.ToTable("ejb_postmeta");

                entity.HasIndex(e => e.MetaKey)
                    .HasName("meta_key");

                entity.HasIndex(e => e.PostId)
                    .HasName("post_id");

                entity.Property(e => e.MetaId).HasColumnName("meta_id");

                entity.Property(e => e.MetaKey)
                    .HasColumnName("meta_key")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.MetaValue)
                    .HasColumnName("meta_value")
                    .HasColumnType("longtext");

                entity.Property(e => e.PostId)
                    .HasColumnName("post_id")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<EjbPosts>(entity =>
            {
                entity.ToTable("ejb_posts");

                entity.HasIndex(e => e.PostAuthor)
                    .HasName("post_author");

                entity.HasIndex(e => e.PostName)
                    .HasName("post_name");

                entity.HasIndex(e => e.PostParent)
                    .HasName("post_parent");

                entity.HasIndex(e => new { e.PostType, e.PostStatus, e.PostDate, e.Id })
                    .HasName("type_status_date");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CommentCount)
                    .HasColumnName("comment_count")
                    .HasColumnType("bigint(20)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.CommentStatus)
                    .IsRequired()
                    .HasColumnName("comment_status")
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'open'");

                entity.Property(e => e.Guid)
                    .IsRequired()
                    .HasColumnName("guid")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.MenuOrder)
                    .HasColumnName("menu_order")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.PingStatus)
                    .IsRequired()
                    .HasColumnName("ping_status")
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'open'");

                entity.Property(e => e.Pinged)
                    .IsRequired()
                    .HasColumnName("pinged")
                    .HasColumnType("text");

                entity.Property(e => e.PostAuthor)
                    .HasColumnName("post_author")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.PostContent)
                    .IsRequired()
                    .HasColumnName("post_content")
                    .HasColumnType("longtext");

                entity.Property(e => e.PostContentFiltered)
                    .IsRequired()
                    .HasColumnName("post_content_filtered")
                    .HasColumnType("longtext");

                entity.Property(e => e.PostDate)
                    .HasColumnName("post_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'0000-00-00 00:00:00'");

                entity.Property(e => e.PostDateGmt)
                    .HasColumnName("post_date_gmt")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'0000-00-00 00:00:00'");

                entity.Property(e => e.PostExcerpt)
                    .IsRequired()
                    .HasColumnName("post_excerpt")
                    .HasColumnType("text");

                entity.Property(e => e.PostMimeType)
                    .IsRequired()
                    .HasColumnName("post_mime_type")
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.PostModified)
                    .HasColumnName("post_modified")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'0000-00-00 00:00:00'");

                entity.Property(e => e.PostModifiedGmt)
                    .HasColumnName("post_modified_gmt")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'0000-00-00 00:00:00'");

                entity.Property(e => e.PostName)
                    .IsRequired()
                    .HasColumnName("post_name")
                    .HasColumnType("varchar(200)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.PostParent)
                    .HasColumnName("post_parent")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.PostPassword)
                    .IsRequired()
                    .HasColumnName("post_password")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.PostStatus)
                    .IsRequired()
                    .HasColumnName("post_status")
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'publish'");

                entity.Property(e => e.PostTitle)
                    .IsRequired()
                    .HasColumnName("post_title")
                    .HasColumnType("text");

                entity.Property(e => e.PostType)
                    .IsRequired()
                    .HasColumnName("post_type")
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'post'");

                entity.Property(e => e.ToPing)
                    .IsRequired()
                    .HasColumnName("to_ping")
                    .HasColumnType("text");
            });

            modelBuilder.Entity<EjbTermmeta>(entity =>
            {
                entity.HasKey(e => e.MetaId);

                entity.ToTable("ejb_termmeta");

                entity.HasIndex(e => e.MetaKey)
                    .HasName("meta_key");

                entity.HasIndex(e => e.TermId)
                    .HasName("term_id");

                entity.Property(e => e.MetaId).HasColumnName("meta_id");

                entity.Property(e => e.MetaKey)
                    .HasColumnName("meta_key")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.MetaValue)
                    .HasColumnName("meta_value")
                    .HasColumnType("longtext");

                entity.Property(e => e.TermId)
                    .HasColumnName("term_id")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<EjbTermRelationships>(entity =>
            {
                entity.HasKey(e => new { e.ObjectId, e.TermTaxonomyId });

                entity.ToTable("ejb_term_relationships");

                entity.HasIndex(e => e.TermTaxonomyId)
                    .HasName("term_taxonomy_id");

                entity.Property(e => e.ObjectId)
                    .HasColumnName("object_id")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.TermTaxonomyId)
                    .HasColumnName("term_taxonomy_id")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.TermOrder)
                    .HasColumnName("term_order")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<EjbTerms>(entity =>
            {
                entity.HasKey(e => e.TermId);

                entity.ToTable("ejb_terms");

                entity.HasIndex(e => e.Name)
                    .HasName("name");

                entity.HasIndex(e => e.Slug)
                    .HasName("slug");

                entity.Property(e => e.TermId).HasColumnName("term_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(200)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Slug)
                    .IsRequired()
                    .HasColumnName("slug")
                    .HasColumnType("varchar(200)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.TermGroup)
                    .HasColumnName("term_group")
                    .HasColumnType("bigint(10)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<EjbTermTaxonomy>(entity =>
            {
                entity.HasKey(e => e.TermTaxonomyId);

                entity.ToTable("ejb_term_taxonomy");

                entity.HasIndex(e => e.Taxonomy)
                    .HasName("taxonomy");

                entity.HasIndex(e => new { e.TermId, e.Taxonomy })
                    .HasName("term_id_taxonomy")
                    .IsUnique();

                entity.Property(e => e.TermTaxonomyId).HasColumnName("term_taxonomy_id");

                entity.Property(e => e.Count)
                    .HasColumnName("count")
                    .HasColumnType("bigint(20)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasColumnType("longtext");

                entity.Property(e => e.Parent)
                    .HasColumnName("parent")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Taxonomy)
                    .IsRequired()
                    .HasColumnName("taxonomy")
                    .HasColumnType("varchar(32)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.TermId)
                    .HasColumnName("term_id")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<EjbUsermeta>(entity =>
            {
                entity.HasKey(e => e.UmetaId);

                entity.ToTable("ejb_usermeta");

                entity.HasIndex(e => e.MetaKey)
                    .HasName("meta_key");

                entity.HasIndex(e => e.UserId)
                    .HasName("user_id");

                entity.Property(e => e.UmetaId).HasColumnName("umeta_id");

                entity.Property(e => e.MetaKey)
                    .HasColumnName("meta_key")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.MetaValue)
                    .HasColumnName("meta_value")
                    .HasColumnType("longtext");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<EjbUsers>(entity =>
            {
                entity.ToTable("ejb_users");

                entity.HasIndex(e => e.UserEmail)
                    .HasName("user_email");

                entity.HasIndex(e => e.UserLogin)
                    .HasName("user_login_key");

                entity.HasIndex(e => e.UserNicename)
                    .HasName("user_nicename");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasColumnName("display_name")
                    .HasColumnType("varchar(250)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.UserActivationKey)
                    .IsRequired()
                    .HasColumnName("user_activation_key")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.UserEmail)
                    .IsRequired()
                    .HasColumnName("user_email")
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.UserLogin)
                    .IsRequired()
                    .HasColumnName("user_login")
                    .HasColumnType("varchar(60)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.UserNicename)
                    .IsRequired()
                    .HasColumnName("user_nicename")
                    .HasColumnType("varchar(50)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.UserPass)
                    .IsRequired()
                    .HasColumnName("user_pass")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.UserRegistered)
                    .HasColumnName("user_registered")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'0000-00-00 00:00:00'");

                entity.Property(e => e.UserStatus)
                    .HasColumnName("user_status")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.UserUrl)
                    .IsRequired()
                    .HasColumnName("user_url")
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''");
            });
        }
    }
}
