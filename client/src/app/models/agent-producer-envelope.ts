import { EnvelopSigners } from "./envelope-signers";
import { Party } from "./party-model";

export class AgentProducerEnvelope {
    constructor(envelopeSigners: EnvelopSigners) {
        this.Agent = new Party(envelopeSigners.agentName, envelopeSigners.agentEmail);
        this.Producer = new Party(envelopeSigners.producerName, envelopeSigners.producerEmail);
        this.DocumentBase64 = envelopeSigners.DocumentBase64
    }
    Agent?: Party;
    Producer?: Party;
    DocumentBase64?: string
}