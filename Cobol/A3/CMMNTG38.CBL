 00001             IDENTIFICATION DIVISION.                                                                              
 00002            *                                                                                                      
 00003             PROGRAM-ID.  CUSTMG38.                                                                                
 00004            *                                                                                                      
 00005             ENVIRONMENT DIVISION.                                                                                 
 00006            *                                                                                                      
 00007             DATA DIVISION.                                                                                        
 00008            *                                                                                                      
 00009             WORKING-STORAGE SECTION.                                                                              
 00010            *                                                                                                      
 00011             01  SWITCHES.                                                                                         
 00012            *                                                                                                      
 00013                 05  VALID-DATA-SW                 PIC X(01) VALUE 'Y'.                                            
 00014                     88  VALID-DATA                          VALUE 'Y'.                                            
 00015            *                                                                                                      
 00016             01  FLAGS.                                                                                            
 00017            *                                                                                                      
 00018                 05  SEND-FLAG                     PIC X(01).                                                      
 00019                     88  SEND-ERASE                       VALUE '1'.                                               
 00020                     88  SEND-ERASE-ALARM                 VALUE '2'.                                               
 00021                     88  SEND-DATAONLY                    VALUE '3'.                                               
 00022                     88  SEND-DATAONLY-ALARM              VALUE '4'.                                               
 00023            *                                                                                                      
 00024             01  WORK-FIELDS.                                                                                      
 00025            *                                                                                                      
 00026                 05  RESPONSE-CODE                 PIC S9(08) COMP.                                                
 00027            *                                                                                                      
000028             01  USER-INSTRUCTIONS.                                                                                
 00029            *                                                                                                      
 00030                 05  ADD-INSTRUCTION               PIC X(79) VALUE                                                 
 00031                     'Type information for new customer.  Then Press Enter.'.                                      
 00032                 05  CHANGE-INSTRUCTION            PIC X(79) VALUE                                                 
 00033                     'Type changes.  Then press Enter.'.                                                           
 00034                 05  DELETE-INSTRUCTION            PIC X(79) VALUE                                                 
 00035                     'Press Enter to delete this customer or press F12 to canc                                     
 00036            -        'el.'.                                                                                        
 00037            *                                                                                                      
 00038             01  COMMUNICATION-AREA.                                                                               
 00039            *                                                                                                      
 00040                 05  CA-CONTEXT-FLAG               PIC X(01).                                                      
 00041                     88  PROCESS-KEY-MAP                  VALUE '1'.                                               
 00042                     88  PROCESS-ADD-CUSTOMER             VALUE '2'.                                               
 00043                     88  PROCESS-CHANGE-CUSTOMER          VALUE '3'.                                               
 00044                     88  PROCESS-DELETE-CUSTOMER          VALUE '4'.                                               
 00045                 05  CA-CUSTOMER-RECORD.                                                                           
 00046                     10  CA-CUSTOMER-NUMBER        PIC X(06).                                                      
 00047                     10  FILLER                    PIC X(112).                                                     
 00048            *                                                                                                      
 00049             01 FILE-STATUS-INFO.                                                                                  
 00050                 05 CMF-OPEN                       PIC X VALUE 'N'.                                                
 00051                 05 CMF-OPEN-STATUS                PIC S9(8) COMP.                                                 
 00052                 05 CMF-ENABLE-STATUS              PIC S9(8) COMP.                                                 
 00053            *                                                                                                      
 00054             COPY CUSTMAS.                                                                                         
 00055            *                                                                                                      
 00056             COPY MNTSG38.                                                                                         
 00057            *                                                                                                      
 00058             COPY DFHAID.                                                                                          
 00059            *                                                                                                      
 00060             COPY ATTR.                                                                                            
 00061            *                                                                                                      
 00062             COPY ERRPARM.                                                                                         
 00063            *                                                                                                      
 00064             LINKAGE SECTION.                                                                                      
 00065            *                                                                                                      
 00066             01  DFHCOMMAREA                       PIC X(119).                                                     
 00067            *                                                                                                      
 00068             PROCEDURE DIVISION.                                                                                   
 00069            *                                                                                                      
 00070             0000-PROCESS-CUSTOMER-MAINT.                                                                          
 00071            *                                                                                                      
 00072                 IF EIBCALEN > ZERO                                                                                
 00073                     MOVE DFHCOMMAREA TO COMMUNICATION-AREA                                                        
 00074                 END-IF.                                                                                           
 00075            *                                                                                                      
 00076                 EVALUATE TRUE                                                                                     
 00077            *                                                                                                      
 00078                     WHEN EIBCALEN = ZERO                                                                          
 00079                         MOVE LOW-VALUE TO MNT1G38O                                                                
 00080                         MOVE -1 TO CUSTNO1L                                                                       
 00081                         SET SEND-ERASE TO TRUE                                                                    
 00082                         PERFORM 1500-SEND-KEY-MAP                                                                 
 00083                         SET PROCESS-KEY-MAP TO TRUE                                                               
000084            * CLOSE FILE IF OPEN ON EXIT                                                                           
 00085                     WHEN EIBAID = DFHPF3 OR DFHPF12                                                               
 00086                         EXEC CICS                                                                                 
 00087                             INQUIRE FILE('CMFG38')                                                                
 00088                                 OPENSTATUS(CMF-OPEN-STATUS)                                                       
 00089                                 ENABLESTATUS(CMF-ENABLE-STATUS)                                                   
 00090                         END-EXEC                                                                                  
 00091                         IF CMF-OPEN-STATUS = DFHVALUE(OPEN)                                                       
 00092                             EXEC CICS                                                                             
 00093                                 SET FILE('CMFG38') CLOSED                                                         
 00094                             END-EXEC                                                                              
 00095                             MOVE 'N' TO CMF-OPEN                                                                  
 00096                         END-IF                                                                                    
 00097            * RETURN TO MENU                                                                                       
 00098                         IF EIBAID = DFHPF3                                                                        
 00099                             EXEC CICS                                                                             
 00100                                 XCTL PROGRAM('UUMENG38')                                                          
 00101                             END-EXEC                                                                              
 00102                         END-IF                                                                                    
 00103            * RETURN TO MENU OR FURTHER PROCESS THINGS                                                             
 00104                         IF EIBAID = DFHPF12                                                                       
 00105                             IF PROCESS-KEY-MAP                                                                    
 00106                                 EXEC CICS                                                                         
 00107                                     XCTL PROGRAM('UUMENG38')                                                      
 00108                                 END-EXEC                                                                          
 00109                             ELSE                                                                                  
 00110                                 MOVE LOW-VALUE TO MNT1G38O                                                        
 00111                                 MOVE -1 TO CUSTNO1L                                                               
 00112                                 SET SEND-ERASE TO TRUE                                                            
 00113                                 PERFORM 1500-SEND-KEY-MAP                                                         
 00114                                 SET PROCESS-KEY-MAP TO TRUE                                                       
 00115                             END-IF                                                                                
 00116                         END-IF                                                                                    
 00117            *                                                                                                      
 00118                     WHEN EIBAID = DFHCLEAR                                                                        
 00119                         IF PROCESS-KEY-MAP                                                                        
 00120                             MOVE LOW-VALUE TO MNT1G38O                                                            
 00121                             MOVE -1 TO CUSTNO1L                                                                   
 00122                             SET SEND-ERASE TO TRUE                                                                
 00123                             PERFORM 1500-SEND-KEY-MAP                                                             
 00124                         ELSE                                                                                      
 00125                             MOVE LOW-VALUE TO MNT2G38O                                                            
 00126                             MOVE CA-CUSTOMER-NUMBER TO CUSTNO2O                                                   
 00127                             EVALUATE TRUE                                                                         
 00128                                 WHEN PROCESS-ADD-CUSTOMER                                                         
 00129                                     MOVE ADD-INSTRUCTION    TO INSTR2O                                            
 00130                                 WHEN PROCESS-CHANGE-CUSTOMER                                                      
 00131                                     MOVE CHANGE-INSTRUCTION TO INSTR2O                                            
 00132                                 WHEN PROCESS-DELETE-CUSTOMER                                                      
 00133                                     MOVE DELETE-INSTRUCTION TO INSTR2O                                            
 00134                             END-EVALUATE                                                                          
 00135                             MOVE -1 TO LNAMEL                                                                     
 00136                             SET SEND-ERASE TO TRUE                                                                
 00137                             PERFORM 1400-SEND-DATA-MAP                                                            
 00138                         END-IF                                                                                    
 00139            *                                                                                                      
000140                     WHEN EIBAID = DFHPA1 OR DFHPA2 OR DFHPA3                                                      
 00141                         CONTINUE                                                                                  
 00142            *                                                                                                      
 00143                     WHEN EIBAID = DFHENTER                                                                        
 00144            * RETRIEVE INITIAL FILE STATUS                                                                         
 00145                         EXEC CICS                                                                                 
 00146                             INQUIRE FILE('CMFG38')                                                                
 00147                                 OPENSTATUS(CMF-OPEN-STATUS)                                                       
 00148                                 ENABLESTATUS(CMF-ENABLE-STATUS)                                                   
 00149                         END-EXEC.                                                                                 
 00150                         IF CMF-OPEN-STATUS = DFHVALUE(OPEN)                                                       
 00151                             MOVE 'Y' TO CMF-OPEN                                                                  
 00152                         ELSE IF CMF-OPEN-STATUS = DFHVALUE(CLOSED)                                                
 00153            * 'N' IS DEFAULT SO NOT REALLY NEEDED...                                                               
 00154                             MOVE 'N' TO CMF-OPEN                                                                  
 00155                         END-IF.                                                                                   
 00156            * PROCESS INPUT                                                                                        
 00157                         EVALUATE TRUE                                                                             
 00158                             WHEN PROCESS-KEY-MAP                                                                  
 00159                                 PERFORM 1000-PROCESS-KEY-MAP                                                      
 00160                             WHEN PROCESS-ADD-CUSTOMER                                                             
 00161                                 PERFORM 2000-PROCESS-ADD-CUSTOMER                                                 
 00162                             WHEN PROCESS-CHANGE-CUSTOMER                                                          
 00163                                 PERFORM 3000-PROCESS-CHANGE-CUSTOMER                                              
 00164                             WHEN PROCESS-DELETE-CUSTOMER                                                          
 00165                                 PERFORM 4000-PROCESS-DELETE-CUSTOMER                                              
 00166            *                                                                                                      
 00167                     WHEN OTHER                                                                                    
 00168                         IF PROCESS-KEY-MAP                                                                        
 00169                             MOVE LOW-VALUE TO MNT1G38O                                                            
 00170                             MOVE 'That key is unassigned.' TO MSG1O                                               
 00171                             MOVE -1 TO CUSTNO1L                                                                   
 00172                             SET SEND-DATAONLY-ALARM TO TRUE                                                       
 00173                             PERFORM 1500-SEND-KEY-MAP                                                             
 00174                         ELSE                                                                                      
 00175                             MOVE LOW-VALUE TO MNT2G38O                                                            
 00176                             MOVE 'That key is unassigned.' TO MSG2O                                               
 00177                             MOVE -1 TO LNAMEL                                                                     
 00178                             SET SEND-DATAONLY-ALARM TO TRUE                                                       
 00179                             PERFORM 1400-SEND-DATA-MAP                                                            
 00180                         END-IF                                                                                    
 00181            *                                                                                                      
 00182                 END-EVALUATE.                                                                                     
 00183                                                                                                                   
 00184                                                                                                                   
 00185                 EXEC CICS                                                                                         
 00186                     RETURN TRANSID('MG38')                                                                        
 00187                            COMMAREA(COMMUNICATION-AREA)                                                           
 00188                 END-EXEC.                                                                                         
 00189            *                                                                                                      
 00190             1000-PROCESS-KEY-MAP.                                                                                 
 00191            *                                                                                                      
 00192                 PERFORM 1100-RECEIVE-KEY-MAP.                                                                     
 00193                 PERFORM 1200-EDIT-KEY-DATA.                                                                       
 00194                 IF VALID-DATA                                                                                     
 00195                     IF NOT PROCESS-DELETE-CUSTOMER                                                                
000196                         INSPECT CUSTOMER-MASTER-RECORD                                                            
 00197                             REPLACING ALL SPACE BY '_'                                                            
 00198                     END-IF                                                                                        
 00199                     MOVE CUSTNO1I      TO CUSTNO2O                                                                
 00200                     MOVE CM-LAST-NAME  TO LNAMEO                                                                  
 00201                     MOVE CM-FIRST-NAME TO FNAMEO                                                                  
 00202                     MOVE CM-ADDRESS    TO ADDRO                                                                   
 00203                     MOVE CM-CITY       TO CITYO                                                                   
 00204                     MOVE CM-STATE      TO STATEO                                                                  
 00205                     MOVE CM-ZIP-CODE   TO ZIPCODEO                                                                
 00206                     MOVE -1 TO LNAMEL                                                                             
 00207                     SET SEND-ERASE TO TRUE                                                                        
 00208                     PERFORM 1400-SEND-DATA-MAP                                                                    
 00209                 ELSE                                                                                              
 00210                     MOVE LOW-VALUE TO CUSTNO1O                                                                    
 00211                                       ACTIONO                                                                     
 00212                     SET SEND-DATAONLY-ALARM TO TRUE                                                               
 00213                     PERFORM 1500-SEND-KEY-MAP                                                                     
 00214                 END-IF.                                                                                           
 00215            *                                                                                                      
 00216             1100-RECEIVE-KEY-MAP.                                                                                 
 00217            *                                                                                                      
 00218                 EXEC CICS                                                                                         
 00219                     RECEIVE MAP('MNT1G38')                                                                        
 00220                             MAPSET('MNTSG38')                                                                     
 00221                             INTO(MNT1G38I)                                                                        
 00222                 END-EXEC.                                                                                         
 00223            *                                                                                                      
 00224                 INSPECT MNT1G38I                                                                                  
 00225                     REPLACING ALL '_' BY SPACE.                                                                   
 00226            *                                                                                                      
 00227             1200-EDIT-KEY-DATA.                                                                                   
 00228            *                                                                                                      
 00229                 MOVE ATTR-NO-HIGHLIGHT TO ACTIONH                                                                 
 00230                                           CUSTNO1H.                                                               
 00231            *                                                                                                      
 00232                 IF ACTIONI NOT = '1' AND '2' AND '3'                                                              
 00233                     MOVE ATTR-REVERSE TO ACTIONH                                                                  
 00234                     MOVE -1 TO ACTIONL                                                                            
 00235                     MOVE 'Action must be 1, 2, or 3.' TO MSG1O                                                    
 00236                     MOVE 'N' TO VALID-DATA-SW                                                                     
 00237                 END-IF.                                                                                           
 00238            *                                                                                                      
 00239                 IF       CUSTNO1L = ZERO                                                                          
 00240                       OR CUSTNO1I = SPACE                                                                         
 00241                     MOVE ATTR-REVERSE TO CUSTNO1H                                                                 
 00242                     MOVE -1 TO CUSTNO1L                                                                           
 00243                     MOVE 'You must enter a customer number.' TO MSG1O                                             
 00244                     MOVE 'N' TO VALID-DATA-SW                                                                     
 00245                 END-IF.                                                                                           
 00246            *                                                                                                      
 00247                 IF VALID-DATA                                                                                     
 00248                     MOVE LOW-VALUE TO MNT2G38O                                                                    
 00249                     EVALUATE ACTIONI                                                                              
 00250                         WHEN '1'                                                                                  
 00251                             PERFORM 1300-READ-CUSTOMER-RECORD                                                     
000252            *                                                                                                      
 00253                             IF RESPONSE-CODE = DFHRESP(NOTFND)                                                    
 00254                                 MOVE ADD-INSTRUCTION TO INSTR2O                                                   
 00255                                 SET PROCESS-ADD-CUSTOMER TO TRUE                                                  
 00256                                 MOVE SPACE TO CUSTOMER-MASTER-RECORD                                              
 00257                             ELSE                                                                                  
 00258                                 IF RESPONSE-CODE = DFHRESP(NORMAL)                                                
 00259                                     MOVE 'That customer already exists.'                                          
 00260                                          TO MSG1O                                                                 
 00261                                     MOVE 'N' TO VALID-DATA-SW                                                     
 00262                                 END-IF                                                                            
 00263                             END-IF                                                                                
 00264                         WHEN '2'                                                                                  
 00265                             PERFORM 1300-READ-CUSTOMER-RECORD                                                     
 00266            *                                                                                                      
 00267                             IF RESPONSE-CODE = DFHRESP(NORMAL)                                                    
 00268                                 MOVE CUSTOMER-MASTER-RECORD TO                                                    
 00269                                     CA-CUSTOMER-RECORD                                                            
 00270                                 MOVE CHANGE-INSTRUCTION TO INSTR2O                                                
 00271                                 SET PROCESS-CHANGE-CUSTOMER TO TRUE                                               
 00272                             ELSE                                                                                  
 00273                                 IF RESPONSE-CODE = DFHRESP(NOTFND)                                                
 00274                                     MOVE 'That customer does not exist.' TO                                       
 00275                                         MSG1O                                                                     
 00276                                     MOVE 'N' TO VALID-DATA-SW                                                     
 00277                                 END-IF                                                                            
 00278                             END-IF                                                                                
 00279                         WHEN '3'                                                                                  
 00280                             PERFORM 1300-READ-CUSTOMER-RECORD                                                     
 00281            *                                                                                                      
 00282                             IF RESPONSE-CODE = DFHRESP(NORMAL)                                                    
 00283                                 MOVE CUSTOMER-MASTER-RECORD TO                                                    
 00284                                     CA-CUSTOMER-RECORD                                                            
 00285                                 MOVE DELETE-INSTRUCTION TO INSTR2O                                                
 00286                                 SET PROCESS-DELETE-CUSTOMER TO TRUE                                               
 00287                                 MOVE ATTR-PROT TO LNAMEA                                                          
 00288                                                   FNAMEA                                                          
 00289                                                   ADDRA                                                           
 00290                                                   CITYA                                                           
 00291                                                   STATEA                                                          
 00292                                                   ZIPCODEA                                                        
 00293                             ELSE                                                                                  
 00294                                 IF RESPONSE-CODE = DFHRESP(NOTFND)                                                
 00295                                     MOVE 'That customer does not exist.' TO                                       
 00296                                         MSG1O                                                                     
 00297                                     MOVE 'N' TO VALID-DATA-SW                                                     
 00298                                 END-IF                                                                            
 00299                             END-IF                                                                                
 00300                     END-EVALUATE.                                                                                 
 00301            *                                                                                                      
 00302             1300-READ-CUSTOMER-RECORD.                                                                            
 00303                                                                                                                   
 00304                 IF CMF-OPEN = 'N'                                                                                 
 00305                     EXEC CICS                                                                                     
 00306                         SET FILE('CMFG38') OPEN                                                                   
 00307                     END-EXEC                                                                                      
000308                     MOVE 'Y' TO CMF-OPEN                                                                          
 00309                 END-IF.                                                                                           
 00310                                                                                                                   
 00311                 EXEC CICS                                                                                         
 00312                     READ FILE('CMFG38')                                                                           
 00313                          INTO(CUSTOMER-MASTER-RECORD)                                                             
 00314                          RIDFLD(CUSTNO1I)                                                                         
 00315                          RESP(RESPONSE-CODE)                                                                      
 00316                 END-EXEC.                                                                                         
 00317            *                                                                                                      
 00318                 IF      RESPONSE-CODE NOT = DFHRESP(NORMAL)                                                       
 00319                     AND RESPONSE-CODE NOT = DFHRESP(NOTFND)                                                       
 00320                     PERFORM 9999-TERMINATE-PROGRAM                                                                
 00321                 END-IF.                                                                                           
 00322            *                                                                                                      
 00323             1400-SEND-DATA-MAP.                                                                                   
 00324            *                                                                                                      
 00325                 MOVE 'MG38' TO TRANID2O.                                                                          
 00326            *                                                                                                      
 00327                 EVALUATE TRUE                                                                                     
 00328                     WHEN SEND-ERASE                                                                               
 00329                         EXEC CICS                                                                                 
 00330                             SEND MAP('MNT2G38')                                                                   
 00331                                  MAPSET('MNTSG38')                                                                
 00332                                  FROM(MNT2G38O)                                                                   
 00333                                  ERASE                                                                            
 00334                                  CURSOR                                                                           
 00335                         END-EXEC                                                                                  
 00336                     WHEN SEND-DATAONLY-ALARM                                                                      
 00337                         EXEC CICS                                                                                 
 00338                             SEND MAP('MNT2G38')                                                                   
 00339                                  MAPSET('MNTSG38')                                                                
 00340                                  FROM(MNT2G38O)                                                                   
 00341                                  DATAONLY                                                                         
 00342                                  ALARM                                                                            
 00343                                  CURSOR                                                                           
 00344                     END-EXEC                                                                                      
 00345                 END-EVALUATE.                                                                                     
 00346            *                                                                                                      
 00347             1500-SEND-KEY-MAP.                                                                                    
 00348            *                                                                                                      
 00349                 MOVE 'MG38' TO TRANID1O.                                                                          
 00350            *                                                                                                      
 00351                 EVALUATE TRUE                                                                                     
 00352                     WHEN SEND-ERASE                                                                               
 00353                         EXEC CICS                                                                                 
 00354                             SEND MAP('MNT1G38')                                                                   
 00355                                  MAPSET('MNTSG38')                                                                
 00356                                  FROM(MNT1G38O)                                                                   
 00357                                  ERASE                                                                            
 00358                                  CURSOR                                                                           
 00359                         END-EXEC                                                                                  
 00360                     WHEN SEND-ERASE-ALARM                                                                         
 00361                         EXEC CICS                                                                                 
 00362                             SEND MAP('MNT1G38')                                                                   
 00363                                  MAPSET('MNTSG38')                                                                
000364                                  FROM(MNT1G38O)                                                                   
 00365                                  ERASE                                                                            
 00366                                  ALARM                                                                            
 00367                                  CURSOR                                                                           
 00368                         END-EXEC                                                                                  
 00369                     WHEN SEND-DATAONLY-ALARM                                                                      
 00370                         EXEC CICS                                                                                 
 00371                             SEND MAP('MNT1G38')                                                                   
 00372                                  MAPSET('MNTSG38')                                                                
 00373                                  FROM(MNT1G38O)                                                                   
 00374                                  DATAONLY                                                                         
 00375                                  ALARM                                                                            
 00376                                  CURSOR                                                                           
 00377                         END-EXEC                                                                                  
 00378                 END-EVALUATE.                                                                                     
 00379            *                                                                                                      
 00380             2000-PROCESS-ADD-CUSTOMER.                                                                            
 00381            *                                                                                                      
 00382                 PERFORM 2100-RECEIVE-DATA-MAP.                                                                    
 00383                 PERFORM 2200-EDIT-CUSTOMER-DATA.                                                                  
 00384                 IF VALID-DATA                                                                                     
 00385                     PERFORM 2300-WRITE-CUSTOMER-RECORD                                                            
 00386            *                                                                                                      
 00387                     IF RESPONSE-CODE = DFHRESP(NORMAL)                                                            
 00388                         MOVE 'Customer record added.' TO MSG1O                                                    
 00389                         SET SEND-ERASE TO TRUE                                                                    
 00390                     ELSE                                                                                          
 00391                         IF RESPONSE-CODE = DFHRESP(DUPREC)                                                        
 00392                             MOVE 'Another user has added a record with that c                                     
 00393            -                    'ustomer number.' TO MSG1O                                                        
 00394                             SET SEND-ERASE-ALARM TO TRUE                                                          
 00395                         END-IF                                                                                    
 00396                     END-IF                                                                                        
 00397                     MOVE -1 TO CUSTNO1L                                                                           
 00398                     PERFORM 1500-SEND-KEY-MAP                                                                     
 00399                     SET PROCESS-KEY-MAP TO TRUE                                                                   
 00400                 ELSE                                                                                              
 00401                     MOVE LOW-VALUE TO LNAMEO                                                                      
 00402                                       FNAMEO                                                                      
 00403                                       ADDRO                                                                       
 00404                                       CITYO                                                                       
 00405                                       STATEO                                                                      
 00406                                       ZIPCODEO                                                                    
 00407                     SET SEND-DATAONLY-ALARM TO TRUE                                                               
 00408                     PERFORM 1400-SEND-DATA-MAP                                                                    
 00409                 END-IF.                                                                                           
 00410            *                                                                                                      
 00411             2100-RECEIVE-DATA-MAP.                                                                                
 00412            *                                                                                                      
 00413                 EXEC CICS                                                                                         
 00414                     RECEIVE MAP('MNT2G38')                                                                        
 00415                             MAPSET('MNTSG38')                                                                     
 00416                             INTO(MNT2G38I)                                                                        
 00417                 END-EXEC.                                                                                         
 00418            *                                                                                                      
 00419                 INSPECT MNT2G38I                                                                                  
000420                     REPLACING ALL '_' BY SPACE.                                                                   
 00421            *                                                                                                      
 00422             2200-EDIT-CUSTOMER-DATA.                                                                              
 00423            *                                                                                                      
 00424                 MOVE ATTR-NO-HIGHLIGHT TO ZIPCODEH                                                                
 00425                                           STATEH                                                                  
 00426                                           CITYH                                                                   
 00427                                           ADDRH                                                                   
 00428                                           FNAMEH                                                                  
 00429                                           LNAMEH.                                                                 
 00430                                                                                                                   
 00431                 IF       ZIPCODEI = SPACE                                                                         
 00432                       OR ZIPCODEL = ZERO                                                                          
 00433                     MOVE ATTR-REVERSE TO ZIPCODEH                                                                 
 00434                     MOVE -1 TO ZIPCODEL                                                                           
 00435                     MOVE 'You must enter a zip code.' TO MSG2O                                                    
 00436                     MOVE 'N' TO VALID-DATA-SW                                                                     
 00437                 END-IF.                                                                                           
 00438                                                                                                                   
 00439                 IF       STATEI = SPACE                                                                           
 00440                       OR STATEL = ZERO                                                                            
 00441                     MOVE ATTR-REVERSE TO STATEH                                                                   
 00442                     MOVE -1 TO STATEL                                                                             
 00443                     MOVE 'You must enter a state.' TO MSG2O                                                       
 00444                     MOVE 'N' TO VALID-DATA-SW                                                                     
 00445                 END-IF.                                                                                           
 00446                                                                                                                   
 00447                 IF       CITYI = SPACE                                                                            
 00448                       OR CITYL = ZERO                                                                             
 00449                     MOVE ATTR-REVERSE TO CITYH                                                                    
 00450                     MOVE -1 TO CITYL                                                                              
 00451                     MOVE 'You must enter a city.' TO MSG2O                                                        
 00452                     MOVE 'N' TO VALID-DATA-SW                                                                     
 00453                 END-IF.                                                                                           
 00454                                                                                                                   
 00455                 IF       ADDRI = SPACE                                                                            
 00456                       OR ADDRL = ZERO                                                                             
 00457                     MOVE ATTR-REVERSE TO ADDRH                                                                    
 00458                     MOVE -1 TO ADDRL                                                                              
 00459                     MOVE 'You must enter an address.' TO MSG2O                                                    
 00460                     MOVE 'N' TO VALID-DATA-SW                                                                     
 00461                 END-IF.                                                                                           
 00462                                                                                                                   
 00463                 IF       FNAMEI = SPACE                                                                           
 00464                       OR FNAMEL = ZERO                                                                            
 00465                     MOVE ATTR-REVERSE TO FNAMEH                                                                   
 00466                     MOVE -1 TO FNAMEL                                                                             
 00467                     MOVE 'You must enter a first name.' TO MSG2O                                                  
 00468                     MOVE 'N' TO VALID-DATA-SW                                                                     
 00469                 END-IF.                                                                                           
 00470                                                                                                                   
 00471                 IF       LNAMEI = SPACE                                                                           
 00472                       OR LNAMEL = ZERO                                                                            
 00473                     MOVE ATTR-REVERSE TO LNAMEH                                                                   
 00474                     MOVE -1 TO LNAMEL                                                                             
 00475                     MOVE 'You must enter a last name.' TO MSG2O                                                   
000476                     MOVE 'N' TO VALID-DATA-SW                                                                     
 00477                 END-IF.                                                                                           
 00478            *                                                                                                      
 00479             2300-WRITE-CUSTOMER-RECORD.                                                                           
 00480            *                                                                                                      
 00481            *                                                                                                      
 00482                 MOVE CUSTNO2I TO CM-CUSTOMER-NUMBER.                                                              
 00483                 MOVE LNAMEI   TO CM-LAST-NAME.                                                                    
 00484                 MOVE FNAMEI   TO CM-FIRST-NAME.                                                                   
 00485                 MOVE ADDRI    TO CM-ADDRESS.                                                                      
 00486                 MOVE CITYI    TO CM-CITY.                                                                         
 00487                 MOVE STATEI   TO CM-STATE.                                                                        
 00488                 MOVE ZIPCODEI TO CM-ZIP-CODE.                                                                     
 00489            *                                                                                                      
 00490                 IF CMF-OPEN = 'N'                                                                                 
 00491                     EXEC CICS                                                                                     
 00492                         SET FILE('CMFG38') OPEN                                                                   
 00493                     END-EXEC                                                                                      
 00494                     MOVE 'Y' TO CMF-OPEN                                                                          
 00495                 END-IF.                                                                                           
 00496            *                                                                                                      
 00497                 EXEC CICS                                                                                         
 00498                     WRITE FILE('CMFG38')                                                                          
 00499                           FROM(CUSTOMER-MASTER-RECORD)                                                            
 00500                           RIDFLD(CM-CUSTOMER-NUMBER)                                                              
 00501                           RESP(RESPONSE-CODE)                                                                     
 00502                 END-EXEC.                                                                                         
 00503            *                                                                                                      
 00504                 IF      RESPONSE-CODE NOT = DFHRESP(NORMAL)                                                       
 00505                     AND RESPONSE-CODE NOT = DFHRESP(DUPREC)                                                       
 00506                     PERFORM 9999-TERMINATE-PROGRAM                                                                
 00507                 END-IF.                                                                                           
 00508            *                                                                                                      
 00509             3000-PROCESS-CHANGE-CUSTOMER.                                                                         
 00510            *                                                                                                      
 00511                 PERFORM 2100-RECEIVE-DATA-MAP.                                                                    
 00512                 PERFORM 2200-EDIT-CUSTOMER-DATA.                                                                  
 00513                 IF VALID-DATA                                                                                     
 00514                     MOVE CUSTNO2I TO CM-CUSTOMER-NUMBER                                                           
 00515                     PERFORM 3100-READ-CUSTOMER-FOR-UPDATE                                                         
 00516            *                                                                                                      
 00517                     IF RESPONSE-CODE = DFHRESP(NORMAL)                                                            
 00518                         IF CUSTOMER-MASTER-RECORD = CA-CUSTOMER-RECORD                                            
 00519                             PERFORM 3200-REWRITE-CUSTOMER-RECORD                                                  
 00520            *                                                                                                      
 00521                             MOVE 'Customer record updated.' TO MSG1O                                              
 00522                             SET SEND-ERASE TO TRUE                                                                
 00523                         ELSE                                                                                      
 00524                             MOVE 'Another user has updated the record.  Try a                                     
 00525            -                     'gain.' TO MSG1O                                                                 
 00526                             SET SEND-ERASE-ALARM TO TRUE                                                          
 00527                         END-IF                                                                                    
 00528                     ELSE                                                                                          
 00529                         IF RESPONSE-CODE = DFHRESP(NOTFND)                                                        
 00530                             MOVE 'Another user has deleted the record.' TO                                        
 00531                                 MSG1O                                                                             
000532                             SET SEND-ERASE-ALARM TO TRUE                                                          
 00533                         END-IF                                                                                    
 00534                     END-IF                                                                                        
 00535                     MOVE -1 TO CUSTNO1L                                                                           
 00536                     PERFORM 1500-SEND-KEY-MAP                                                                     
 00537                     SET PROCESS-KEY-MAP TO TRUE                                                                   
 00538                 ELSE                                                                                              
 00539                     MOVE LOW-VALUE TO LNAMEO                                                                      
 00540                                       FNAMEO                                                                      
 00541                                       ADDRO                                                                       
 00542                                       CITYO                                                                       
 00543                                       STATEO                                                                      
 00544                                       ZIPCODEO                                                                    
 00545                     SET SEND-DATAONLY-ALARM TO TRUE                                                               
 00546                     PERFORM 1400-SEND-DATA-MAP                                                                    
 00547                 END-IF.                                                                                           
 00548            *                                                                                                      
 00549             3100-READ-CUSTOMER-FOR-UPDATE.                                                                        
 00550            *                                                                                                      
 00551                 IF CMF-OPEN = 'N'                                                                                 
 00552                     EXEC CICS                                                                                     
 00553                         SET FILE('CMFG38') OPEN                                                                   
 00554                     END-EXEC                                                                                      
 00555                     MOVE 'Y' TO CMF-OPEN                                                                          
 00556                 END-IF.                                                                                           
 00557                 EXEC CICS                                                                                         
 00558                     READ FILE('CMFG38')                                                                           
 00559                          INTO(CUSTOMER-MASTER-RECORD)                                                             
 00560                          RIDFLD(CM-CUSTOMER-NUMBER)                                                               
 00561                          UPDATE                                                                                   
 00562                          RESP(RESPONSE-CODE)                                                                      
 00563                 END-EXEC.                                                                                         
 00564            *                                                                                                      
 00565                 IF      RESPONSE-CODE NOT = DFHRESP(NORMAL)                                                       
 00566                     AND RESPONSE-CODE NOT = DFHRESP(NOTFND)                                                       
 00567                     PERFORM 9999-TERMINATE-PROGRAM                                                                
 00568                 END-IF.                                                                                           
 00569            *                                                                                                      
 00570             3200-REWRITE-CUSTOMER-RECORD.                                                                         
 00571            *                                                                                                      
 00572                 MOVE LNAMEI   TO CM-LAST-NAME.                                                                    
 00573                 MOVE FNAMEI   TO CM-FIRST-NAME.                                                                   
 00574                 MOVE ADDRI    TO CM-ADDRESS.                                                                      
 00575                 MOVE CITYI    TO CM-CITY.                                                                         
 00576                 MOVE STATEI   TO CM-STATE.                                                                        
 00577                 MOVE ZIPCODEI TO CM-ZIP-CODE.                                                                     
 00578            *                                                                                                      
 00579                 IF CMF-OPEN = 'N'                                                                                 
 00580                     EXEC CICS                                                                                     
 00581                         SET FILE('CMFG38') OPEN                                                                   
 00582                     END-EXEC                                                                                      
 00583                     MOVE 'Y' TO CMF-OPEN                                                                          
 00584                 END-IF.                                                                                           
 00585                 EXEC CICS                                                                                         
 00586                     REWRITE FILE('CMFG38')                                                                        
 00587                             FROM(CUSTOMER-MASTER-RECORD)                 
000588                             RESP(RESPONSE-CODE)                                                                   
 00589                 END-EXEC.                                                                                         
 00590            *                                                                                                      
 00591                 IF RESPONSE-CODE NOT = DFHRESP(NORMAL)                                                            
 00592                     PERFORM 9999-TERMINATE-PROGRAM                                                                
 00593                 END-IF.                                                                                           
 00594            *                                                                                                      
 00595             4000-PROCESS-DELETE-CUSTOMER.                                                                         
 00596            *                                                                                                      
 00597                 MOVE CA-CUSTOMER-NUMBER TO CM-CUSTOMER-NUMBER.                                                    
 00598                 PERFORM 3100-READ-CUSTOMER-FOR-UPDATE.                                                            
 00599            *                                                                                                      
 00600                 IF RESPONSE-CODE = DFHRESP(NORMAL)                                                                
 00601                     IF CUSTOMER-MASTER-RECORD = CA-CUSTOMER-RECORD                                                
 00602                         PERFORM 4100-DELETE-CUSTOMER-RECORD                                                       
 00603                         MOVE 'Customer deleted.' TO MSG1O                                                         
 00604                         SET SEND-ERASE TO TRUE                                                                    
 00605                     ELSE                                                                                          
 00606                         MOVE 'Another user has updated the record.  Try again                                     
 00607            -                 '.' TO MSG1O                                                                         
 00608                         SET SEND-ERASE-ALARM TO TRUE                                                              
 00609                     END-IF                                                                                        
 00610                 ELSE                                                                                              
 00611                     IF RESPONSE-CODE = DFHRESP(NOTFND)                                                            
 00612                         MOVE 'Another user has deleted the record.' TO                                            
 00613                             MSG1O                                                                                 
 00614                         SET SEND-ERASE-ALARM TO TRUE                                                              
 00615                     END-IF                                                                                        
 00616                 END-IF.                                                                                           
 00617                 MOVE -1 TO CUSTNO1L.                                                                              
 00618                 PERFORM 1500-SEND-KEY-MAP.                                                                        
 00619                 SET PROCESS-KEY-MAP TO TRUE.                                                                      
 00620            *                                                                                                      
 00621             4100-DELETE-CUSTOMER-RECORD.                                                                          
 00622            *                                                                                                      
 00623                 IF CMF-OPEN = 'N'                                                                                 
 00624                     EXEC CICS                                                                                     
 00625                         SET FILE('CMFG38') OPEN                                                                   
 00626                     END-EXEC                                                                                      
 00627                     MOVE 'Y' TO CMF-OPEN                                                                          
 00628                 END-IF.                                                                                           
 00629                 EXEC CICS                                                                                         
 00630                     DELETE FILE('CMFG38')                                                                         
 00631                            RESP(RESPONSE-CODE)                                                                    
 00632                 END-EXEC.                                                                                         
 00633            *                                                                                                      
 00634                 IF  RESPONSE-CODE NOT = DFHRESP(NORMAL)                                                           
 00635                     PERFORM 9999-TERMINATE-PROGRAM                                                                
 00636                 END-IF.                                                                                           
 00637            *                                                                                                      
 00638             9999-TERMINATE-PROGRAM.                                                                               
 00639            *                                                                                                      
 00640                 MOVE EIBRESP  TO ERR-RESP.                                                                        
 00641                 MOVE EIBRESP2 TO ERR-RESP2.                                                                       
 00642                 MOVE EIBTRNID TO ERR-TRNID.                                                                       
 00643                 MOVE EIBRSRCE TO ERR-RSRCE.                                                                                       
000644            *                                                                                                      
 00645                 EXEC CICS                                                                                         
 00646                     XCTL PROGRAM('SYSERR')                                                                        
 00647                          COMMAREA(ERROR-PARAMETERS)                                                               
 00648                 END-EXEC.                                                                                         
