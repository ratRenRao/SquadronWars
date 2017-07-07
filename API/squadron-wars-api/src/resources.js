var findById = {
  'spec': {
    "description" : "Used to validate logins",
    "path" : "/login",
    "notes" : "",
    "summary" : "Find pet by ID",
    "method": "GET",
    "parameters" : [swagger.pathParam("user", "username for login", "string"), swagger.pathParam("pass", "password for login", "string")],
    "type" : "Login",
    "errorResponses" : [swagger.errors.invalid('pass'), swagger.errors.notFound('user')],
    "nickname" : "login"
  },

  'action': function (req,res) {
    if (!req.params.petId) {
      throw swagger.errors.invalid('id');
    }
    var id = parseInt(req.params.petId);
    var pet = petData.getPetById(id);
 
    if (pet) {
      res.send(JSON.stringify(pet));
    } else {
      throw swagger.errors.notFound('pet');
    }
  }
};
 
swagger.addGet(findById);
