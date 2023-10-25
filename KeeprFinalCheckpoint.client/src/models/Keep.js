

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

export class VaultKeep extends Keep{
  constructor(data){
    super(data)
    this.vaultKeepId = data.vaultKeepId
    this.vaultKeepCreatorId = data.profileId
  }
}