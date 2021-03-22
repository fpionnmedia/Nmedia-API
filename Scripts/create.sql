CREATE TABLE "Nmedians"
(
  "Age" SMALLINT NULL,
  "Created" TIMESTAMP WITH TIME ZONE NOT NULL,
  "Hired" DATE NULL,
  "HourlyRate" DECIMAL(5,2) NULL,
  "Id" SERIAL NOT NULL,
  "IsActive" BOOLEAN NOT NULL,
  "JobTitle" INT NULL,
  "Name" VARCHAR(100) NOT NULL,
  "Picture" TEXT NULL,
  "Slug" VARCHAR(100) NULL,
  "Updated" TIMESTAMP WITH TIME ZONE NULL,
  "Uuid" UUID NOT NULL,
  CONSTRAINT "PK_Nmedians" PRIMARY KEY ("Id"),
  CONSTRAINT "UQ_Nmedians_Slug" UNIQUE ("Slug"),
  CONSTRAINT "UQ_Nmedians_Uuid" UNIQUE ("Uuid"),
  CONSTRAINT "CC_Nmedians_Age" CHECK ("Age" > 0)
);

CREATE TABLE "Articles"
(
  "Categories" INT[] NULL,
  "Content" TEXT NULL,
  "Created" TIMESTAMP WITH TIME ZONE NOT NULL,
  "Id" SERIAL NOT NULL,
  "NmedianId" INT NULL,
  "Picture" TEXT NULL,
  "Published" DATE NULL,
  "Title" VARCHAR(200) NOT NULL,
  "Updated" TIMESTAMP WITH TIME ZONE NULL,
  "Uuid" UUID NOT NULL,
  CONSTRAINT "PK_Articles" PRIMARY KEY ("Id"),
  CONSTRAINT "UQ_Articles_Uuid" UNIQUE ("Uuid"),
  CONSTRAINT "FK_Articles_NmedianId" FOREIGN KEY ("NmedianId") REFERENCES "Nmedians" ("Id") ON DELETE CASCADE
);
