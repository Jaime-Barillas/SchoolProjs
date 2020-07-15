       IDENTIFICATION DIVISION.
      *
       PROGRAM-ID.  GETING38.
      *
       ENVIRONMENT DIVISION.
      *
       DATA DIVISION.
      *
       WORKING-STORAGE SECTION.
      *
       01 FILE-STATUS-INFO.
           05 CTL-OPEN           PIC X VALUE 'N'.
           05 CTL-OPEN-STATUS    PIC S9(8) COMP.
           05 CTL-ENABLE-STATUS  PIC S9(8) COMP.
      *
       COPY INVCTL.
      *
       LINKAGE SECTION.
      *
       01  DFHCOMMAREA   PIC 9(06).
      *
       PROCEDURE DIVISION.
      *
       0000-GET-INVOICE-NUMBER.
      *
           MOVE ZERO TO INVCTL-RECORD-KEY.
      *
           EXEC CICS
               INQUIRE FILE('CTLG38')
                   OPENSTATUS(CTL-OPEN-STATUS)
                   ENABLESTATUS(CTL-ENABLE-STATUS)
           END-EXEC.
           IF CTL-OPEN-STATUS = DFHVALUE(CLOSED)
               EXEC CICS
                   SET FILE('CTLG38') OPEN
               END-EXEC
               MOVE 'Y' TO CTL-OPEN
           END-IF.
      *
           EXEC CICS
               READ FILE('CTLG38')
                    INTO(INVCTL-RECORD)
                    RIDFLD(INVCTL-RECORD-KEY)
                    UPDATE
           END-EXEC.
           MOVE INVCTL-NEXT-INVOICE-NUMBER TO DFHCOMMAREA.
           ADD 1 TO INVCTL-NEXT-INVOICE-NUMBER.
           EXEC CICS
               REWRITE FILE('CTLG38')
                       FROM(INVCTL-RECORD)
           END-EXEC.
      *
           IF CTL-OPEN = 'Y'
               EXEC CICS
                   SET FILE('CTLG38') CLOSED
               END-EXEC
               MOVE 'N' TO CTL-OPEN
           END-IF.
      *
           EXEC CICS
               RETURN
           END-EXEC.
