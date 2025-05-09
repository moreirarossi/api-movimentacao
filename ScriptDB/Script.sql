USE MovimentosManuais;
GO

IF NOT EXISTS( SELECT * FROM SYS.sysobjects WHERE name = 'PRODUTO' ) 
	CREATE TABLE dbo.PRODUTO
	(
		COD_PRODUTO CHAR(4) NOT NULL,
		DES_PRODUTO VARCHAR(30) NULL,
		STA_STATUS CHAR(1) NULL,
		PRIMARY KEY (COD_PRODUTO)
	);
GO

IF NOT EXISTS( SELECT * FROM SYS.sysobjects WHERE name = 'PRODUTO_COSIF' ) 
	CREATE TABLE dbo.PRODUTO_COSIF
	(
		COD_PRODUTO CHAR(4) NOT NULL,
		COD_COSIF CHAR(11) NOT NULL,
		COD_CLASSIFICACAO CHAR(6) NULL,
		STA_STATUS CHAR(1) NULL,
		PRIMARY KEY (COD_PRODUTO, COD_COSIF)
	);
GO

IF NOT EXISTS( SELECT * FROM SYS.SYSOBJECTS WHERE name = 'FK_PRODUTO_COSIF_PRODUTO' ) 
	ALTER TABLE PRODUTO_COSIF 
	ADD CONSTRAINT FK_PRODUTO_COSIF_PRODUTO 
	FOREIGN KEY (COD_PRODUTO) REFERENCES PRODUTO(COD_PRODUTO);
GO

IF NOT EXISTS( SELECT * FROM SYS.sysobjects WHERE name = 'MOVIMENTO_MANUAL' ) 
	CREATE TABLE dbo.MOVIMENTO_MANUAL
	(
		DAT_MES NUMERIC(2,0) NOT NULL,
		DAT_ANO NUMERIC(4,0) NOT NULL,
		NUM_LANCAMENTO NUMERIC(18,0) NOT NULL,
		COD_PRODUTO CHAR(4) NOT NULL,
		COD_COSIF CHAR(11) NOT NULL,
		DES_DESCRICAO VARCHAR(50) NOT NULL,
		DAT_MOVIMENTO SMALLDATETIME NOT NULL default(GETDATE()),
		COD_USUARIO VARCHAR(15) NOT NULL default(suser_sname()),
		VAL_VALOR NUMERIC(18,2) NOT NULL,
		PRIMARY KEY (DAT_ANO, DAT_MES, NUM_LANCAMENTO)
	);
GO

IF NOT EXISTS( SELECT * FROM SYS.SYSOBJECTS WHERE name = 'FK_MOVIMENTO_MANUAL_PRODUTO_COSIF' ) 
	ALTER TABLE MOVIMENTO_MANUAL
	ADD CONSTRAINT FK_MOVIMENTO_MANUAL_PRODUTO_COSIF
	FOREIGN KEY (COD_PRODUTO, COD_COSIF) REFERENCES PRODUTO_COSIF(COD_PRODUTO, COD_COSIF);
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.SP_MOVIMENTOS_MANUAIS') AND type = N'P')
BEGIN
    EXEC(' CREATE PROCEDURE dbo.SP_MOVIMENTOS_MANUAIS AS BEGIN SELECT 1; END ');
END;
GO

ALTER PROCEDURE dbo.SP_MOVIMENTOS_MANUAIS
AS
BEGIN
  SELECT m.DAT_MES,
		 m.DAT_ANO,
		 m.COD_PRODUTO,
		 p.DES_PRODUTO,
		 m.NUM_LANCAMENTO,
		 m.DES_DESCRICAO,
		 m.VAL_VALOR
  FROM MOVIMENTO_MANUAL m
    INNER JOIN PRODUTO p ON (p.COD_PRODUTO = m.COD_PRODUTO)
  ORDER BY m.DAT_ANO,
		   m.DAT_MES,
		   m.NUM_LANCAMENTO
END
GO

IF NOT EXISTS( SELECT * FROM PRODUTO )
BEGIN
	INSERT INTO PRODUTO ( COD_PRODUTO, DES_PRODUTO, STA_STATUS ) 
	VALUES  ( '0001', 'PRODUTO 001', 'A' ), 
			( '0002', 'PRODUTO 002', 'A' ), 
			( '0003', 'PRODUTO 003', 'A' ), 
			( '0004', 'PRODUTO 004', 'I' ), 
			( '0005', 'PRODUTO 005', 'A' );

	INSERT INTO PRODUTO_COSIF( COD_PRODUTO, COD_COSIF, COD_CLASSIFICACAO, STA_STATUS ) 
	VALUES  ( '0001', '1.1.1.01.00', '121', 'A' ),
			( '0001', '1.2.3.05.00', '122', 'A' ),
			( '0001', '1.1.2.10.00', '123', 'A' ),
			( '0001', '4.1.1.10.00', '124', 'I' ),
			( '0001', '1.2.1.30.00', '121', 'A' ),
			( '0002', '1.1.1.01.00', '122', 'A' ),
			( '0002', '1.2.3.05.00', '123', 'A' ),
			( '0002', '1.1.2.10.00', '124', 'A' ),
			( '0003', '4.1.1.10.00', '121', 'I' ),
			( '0003', '1.2.1.30.00', '122', 'A' ),
			( '0004', '1.1.1.01.00', '123', 'A' ),
			( '0004', '1.2.3.05.00', '124', 'A' ),
			( '0005', '1.1.2.10.00', '121', 'A' ),
			( '0005', '4.1.1.10.00', '122', 'I' ),
			( '0005', '1.2.1.30.00', '123', 'A' );
END;

