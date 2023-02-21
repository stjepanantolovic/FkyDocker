import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
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
  defaultValuesChecked=false;
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
      defaultValues: new FormControl(),
      documentBase64: new FormControl()
    }
    )
  }

  sendEnvelope() {
    this.model = Object.assign({}, this.envelopeForm.value) as EnvelopeSigners;  
    this.model.documentBase64 = undefined;
    this.model.defaultValues = undefined;
    this.evelopeService.testDocuSigWebHook(this.model);
    this.model.DocumentBase64 = this.base64String;
    this.evelopeService.sendEnvelope(this.model);
  }

  onUpload(event: any) {   
    var reader = new FileReader();
    var file = event.target.files[0];   
    reader.readAsDataURL(file);
  
    
    reader.onload = () => {
      console.log(reader.result);
      this.base64String = reader.result as string;
      this.base64String = this.base64String.split('base64,')[1];
      
  }; 
    
  }

  onDefault($event:any){    
    var defaultValuesChecked =this.envelopeForm.controls['defaultValues'].value;    
    if(defaultValuesChecked){
      this.setFormDefaultValues();
      this.defaultValuesChecked=defaultValuesChecked;
    }
    else{
      if(this.defaultValuesChecked!=defaultValuesChecked){
        this.envelopeForm.reset();
        this.defaultValuesChecked=defaultValuesChecked;
      }
    }
  }


  setFormDefaultValues(){
    this.envelopeForm.controls['agentName'].setValue('Phil Jackson');
    this.envelopeForm.controls['agentEmail'].setValue('proag.livestock@gmail.com');
    this.envelopeForm.controls['producerName'].setValue('Scottie Pippen');
    this.envelopeForm.controls['producerEmail'].setValue('proaglivestock2@gmail.com');
  }

 

}




