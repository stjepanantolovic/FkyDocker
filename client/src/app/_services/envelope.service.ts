import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { BehaviorSubject } from 'rxjs';
import { AgentProducerEnvelope } from '../models/agent-producer-envelope';
import { EnvelopSigners as EnvelopeSigners } from '../models/envelope-signers';

@Injectable({
  providedIn: 'root'
})
export class EnvelopeService {
  baseUrl = environment.appiUrl + 'envelope/';
  constructor(private http: HttpClient) { }

  sendEnvelope(request: EnvelopeSigners) {
    console.log('sendEnvelope Service EnvelopeSigners', request);
    var agentProducerEnvelope = new AgentProducerEnvelope(request);
    console.log('sendEnvelope Service agentProducerEnvelope', agentProducerEnvelope);
    this.http.post(this.baseUrl + 'sendEnvelope/', agentProducerEnvelope)
    .subscribe((response: any) => {
          console.log('SendEnvelope response', response);
        });   
    
  }

  testDocuSigWebHook(webHook:any){
    this.http.post(this.baseUrl + 'DocuSigWebHook/', webHook)
    .subscribe((response: any) => {
          console.log('SendEnvelope response', response);
        });
  }
}

