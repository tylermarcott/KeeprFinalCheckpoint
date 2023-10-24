

export class Keep{
  constructor(data){
    this.id = data.id
    this.creatorId = data.creatorId
    this.name = data.name
    this.description = data. description
    this.img = data.img
    this.views = data.views
    this.kept = data.kept
    this.creator = data.creator
  }
}
// FIXME: have to be able to add vaultKeep id to a keep, just like we do in the backend. Check instacult I think for ref.

// export class VaultKeep extends Keep{
//   constructor(){

//   }
// }