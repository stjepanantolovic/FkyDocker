import { HttpClient } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { AgGridAngular } from 'ag-grid-angular';
import { CellClickedEvent, ColDef, GridOptions, GridReadyEvent } from 'ag-grid-community';
import { Observable } from 'rxjs';
import { Envelope } from 'src/app/models/envelope-model';
import { EnvelopeService } from 'src/app/_services/envelope.service';
@Component({
  selector: 'app-envelope-grid',
  templateUrl: './envelope-grid.component.html',
  styleUrls: ['./envelope-grid.component.css']
})
export class EnvelopeGridComponent implements OnInit {
  @ViewChild(AgGridAngular) agGrid!: AgGridAngular;
  public rowData$!: Observable<any[]>;

  constructor(private envelopeService: EnvelopeService) { }

  ngOnInit(): void {
  }

  public columnDefs: ColDef[] = [
    { headerName: "Row",
    valueGetter: "node.rowIndex + 1", width:10},
    { field: 'id', headerName: 'ID' },
    { field: 'docuSignId', headerName: 'DocuSignId' },
    { field: 'agentSignTimeStamp', headerName: 'Agent Signature TimeStamp' },
    { field: 'envelopeSentToDocuSignTimeStamp', headerName: 'Envelope Sent TimeStamp' },
    { field: 'envelopeCompletedTimeStamp', headerName: 'Envelope Completed TimeStamp' }
  ];

  public defaultColDef: ColDef = {
    sortable: true,
    filter: true,
  };

subscribeToEnvelopeSave(){
  this.envelopeService.envelopeSentEmitter.subscribe(response=>{
    console.log('event emitter received', response);
    this.rowData$ = this.envelopeService.getEnvelopes() as Observable<any[]>;
    this.agGrid.api.sizeColumnsToFit();
  })
}




  onGridReady(params: GridReadyEvent) {
    this.rowData$ = this.envelopeService.getEnvelopes() as Observable<any[]>;
    console.log('onGridReady', params);
    this.agGrid.api.sizeColumnsToFit();
  }


  onCellClicked(e: CellClickedEvent): void {
    console.log('cellClicked', e);
  }


  clearSelection(): void {
    this.agGrid.api.deselectAll();
  }
}
