import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { AgentProducerEnvelope } from 'src/app/models/agent-producer-envelope';
import { EnvelopSigners as EnvelopeSigners } from 'src/app/models/envelope-signers';
import { EnvelopeService } from 'src/app/_services/envelope.service';

@Component({
  selector: 'app-envelope',
  templateUrl: './envelope.component.html',
  styleUrls: ['./envelope.component.css']
})
export class EnvelopeComponent implements OnInit {
  envelopeForm: FormGroup = new FormGroup({});
  model: EnvelopeSigners | any;
  base64String = "";
  constructor(private evelopeService: EnvelopeService) { }

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {
    this.envelopeForm = new FormGroup({
      agentName: new FormControl(),
      agentEmail: new FormControl(),
      producerName: new FormControl(),
      producerEmail: new FormControl(),
      documentBase64: new FormControl()
    }
    )
  }

  sendEnvelope() {
    this.model = Object.assign({}, this.envelopeForm.value) as EnvelopeSigners;
    this.model.agentName= "Phil Jackson";
    this.model.agentEmail= "Proag.livestock@gmail.com";
    this.model.producerName="Scottie Pippen";
    this.model.producerEmail="proaglivestock2@gmail.com";
    this.model.DocumentBase64 = this.base64String;
    console.log(this.model);
    this.evelopeService.sendEnvelope(this.model);
  }

  onUpload(event: any) {
    console.log('file event', event);
    var reader = new FileReader();
    var file = event.target.files[0];
    console.log('file', file);
    reader.readAsDataURL(file);
    console.log('reader', reader);
    console.log('reader.result', reader.result);
    
    reader.onload = () => {
      console.log(reader.result);
      this.base64String = reader.result as string;
  };
    
    
  }

}
